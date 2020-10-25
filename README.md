[中文](https://github.com/TROU2004/WonderfulMoments/blob/master/README_CN.md) [English](https://github.com/TROU2004/WonderfulMoments/blob/master/README.md)
# WonderfulMoments
WonderfulMoments can automatically guide your obs when to start the recording so that you can manage your highlight moments.
It works easily like a bridge between osu! and obs. Thanks to OsuRTDataProvider provides the ways to check your osu state.

## Downloads
WonderfulMoments for Windows is available in the [Releases](https://github.com/TROU2004/WonderfulMoments/releases) section.
Other dependencies can be found in the Releases section in detailed

## Using WonderfulMoments
At first you need to prepare a osu! game and a obs software, they can be easily solved by Google search.

1. You need to install obs-websocket plugin so that we can control your obs by WonderfulMoments automatically. Downloads can be found in Releases section
1. You have to prepare a Sync-Tools because WonderfulMoments is a plugin of it.
    - If you have used Sync-Tools before. You can download "WonderfulMoments.zip" and put the unzipped files to the Plugins directory. (You need a ORTDP as dependency first, you can type "plugins install ortdp" to install it)
    - If you don't know what is Sync-Tools. You can easily download the "Sync2.18.5.zip" and Run "Sync.exe" directly. (Remember, Sync-Tools is a console-like application, if you close the console window, WonderfulMoments will stop working)
1. You can enjoy playing with WonderfulMoments. Each map you have played will be record by obs and saved in your obs recording directory. Only ten videos can be saved(You can change it in the config) in order to save your space.

## Configuration
You can simply change the config of WonderfulMoments by type "config" in Sync Console Window. Just unfold "WonderfulMoments-Setting" you can see the configs.
- WebSocketURL and Password: Just check your obs "Tools - WebSockets Server Settings". Password can be empty when you disable the password verify.
- FullMode: WonderfulMoments can work in FullMode or PartMode, PartMode is the default value
    - FullMode: obs will record a whole video of your osu play include many map-plays in a video. The video will be paused when you select maps or other situation. After your osu!life tonight, you will only get a big video file.
    - PartMode: obs will record videos of each osu play. obs will start at the beginning of the map-play, and stop at the end of the map-play. After your osu!life tonight, you will get many short video files.
    - **Important: Only mp4 output can be used in FullMode and PartMode, because flv could't pause during a recording**
- VideosLimit(Only works in PartMode): The number of videos you want to keep, old videos will be removed if your plays over this number
