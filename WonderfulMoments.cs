using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using OsuRTDataProvider;
using Sync.Plugins;
using Sync.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static OsuRTDataProvider.Listen.OsuListenerManager;

namespace WonderfulMoments
{

    [SyncPluginDependency("7216787b-507b-4eef-96fb-e993722acf2e", Version = "^1.6.5", Require = true)] //RTPPD 1.6.5+
    [SyncPluginID("0d48b9cd-2616-45a1-b44e-9fae8a945303", "1.0.0")]
    public class WonderfulMoments : Plugin
    {
        readonly PluginConfigurationManager config_manager;
        public static OBSWebsocket obs = new OBSWebsocket();
        OsuRTDataProviderPlugin ortdp;
        OutputState outputState;
        bool isPaused;
        OsuRTDataProvider.BeatmapInfo.Beatmap beatmap;
        Queue<string> videos = new Queue<string>();

        public WonderfulMoments() : base("WonderfulMoments", "TuRou")
        {
            config_manager = new PluginConfigurationManager(this);
            config_manager.AddItem(new Setting());
            EventBus.BindEvent<PluginEvents.LoadCompleteEvent>(OnLoadComplete);
            obs.RecordingStateChanged += Obs_RecordingStateChanged;
            obs.RecordingResumed += Obs_RecordingResumed;
            obs.RecordingPaused += Obs_RecordingPaused;
        }

        private void Obs_RecordingPaused(object sender, System.EventArgs e)
        {
            isPaused = true;
        }

        private void Obs_RecordingResumed(object sender, System.EventArgs e)
        {
            isPaused = false;
        }

        private void Obs_RecordingStateChanged(OBSWebsocket sender, OutputState type)
        {
            outputState = type;
        }

        private void OnLoadComplete(PluginEvents.LoadCompleteEvent @event)
        {
            ortdp = (OsuRTDataProviderPlugin)getHoster().EnumPluings().FirstOrDefault(p => p.Name == "OsuRTDataProvider");
            ortdp.ListenerManager.OnStatusChanged += ListenerManager_OnStatusChanged;
            ortdp.ListenerManager.OnBeatmapChanged += ListenerManager_OnBeatmapChanged;
            Task.Run(CheckProcess);
        }

        private void ListenerManager_OnBeatmapChanged(OsuRTDataProvider.BeatmapInfo.Beatmap map)
        {
            beatmap = map;
        }

        private void ListenerManager_OnStatusChanged(OsuStatus last_status, OsuStatus status)
        {
            if (Setting.SettingValue.FullMode)
            {
                //Game is started
                if (obs.IsConnected && last_status == OsuStatus.Unkonwn && status == OsuStatus.Playing)
                {
                    IO.CurrentIO.Write("attemp to start");
                    obs.StartRecording();
                    return;
                }
                // Select a map and start playing it
                if (status == OsuStatus.Playing && last_status != OsuStatus.Playing && isPaused)
                {
                    obs.ResumeRecording();
                    return;
                }
                // Rank a score and back to selecting page
                if (outputState == OutputState.Started && last_status == OsuStatus.Rank && status != OsuStatus.Rank && !isPaused)
                {
                    obs.PauseRecording();
                    return;
                }
                // Play a map and then get failed or exit
                if (outputState == OutputState.Started && last_status == OsuStatus.Playing && status != OsuStatus.Playing && status != OsuStatus.Rank && !isPaused)
                {
                    obs.PauseRecording();
                    return;
                }
            }
            else
            {
                if (obs.IsConnected && last_status != OsuStatus.Playing && status == OsuStatus.Playing)
                {
                    var name = DateTime.Now.ToString("[yyyy-MM-dd HH-mm-ss]") + beatmap.Title;
                    var regex = new Regex(@"^[^\/\:\*\?\""\<\>\|\,]+$");
                    if (!regex.Match(name).Success)
                    {
                        name = DateTime.Now.ToString("[yyyy-MM-dd HH-mm-ss]") + beatmap.BeatmapID;
                    }
                    videos.Enqueue(name);
                    obs.SetFilenameFormatting(name);
                    obs.StartRecording();
                    return;
                }
                if (obs.IsConnected && last_status == OsuStatus.Rank && status != OsuStatus.Rank)
                {
                    HalfStop();
                }
                if (obs.IsConnected && last_status == OsuStatus.Playing && status != OsuStatus.Rank)
                {
                    HalfStop();
                }
            }
        }

        private void CheckProcess()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (!obs.IsConnected)
                {
                    if (Process.GetProcessesByName("obs32").Length + Process.GetProcessesByName("obs64").Length != 0)
                    {
                        obs.Connect(Setting.SettingValue.WebSocketURL, Setting.SettingValue.Password);
                    }
                }
                if (obs.IsConnected && Setting.SettingValue.FullMode)
                {
                    if (Process.GetProcessesByName("osu!").Length == 0)
                    {
                        obs.StopRecording();
                    }
                }
            }
        }

        private void HalfStop()
        {
            obs.StopRecording();
            if (videos.Count > Setting.SettingValue.Limit)
            {
                File.Delete(obs.GetRecordingFolder() + "\\" + videos.Dequeue() + ".mp4");
            }
            obs.SetFilenameFormatting("%CCYY-%MM-%DD %hh-%mm-%ss");
        }
    }
}
