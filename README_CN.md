[中文](https://github.com/TROU2004/WonderfulMoments/blob/master/README_CN.md) [English](https://github.com/TROU2004/WonderfulMoments/blob/master/README.md)
# WonderfulMoments 精彩时刻
WonderfulMoments可以控制你的obs自动地开始录制或者停止录制你的osu画面. 配合上ORTDP(OsuRTDataProvider)来实时读取osu!内数据, 可以做到记录你屙屎中精彩时刻的效果.
用WonderfulMoments以后就可以不需要录制很长的屙屎视频之后再找精彩的部分剪辑, 打到好成绩以后可以直接把视频给朋友发过去, 很方便.

## 下载
可以直接到[Releases](https://github.com/TROU2004/WonderfulMoments/releases)中下载最新发布的WonderfulMoments.一些需要的依赖程序也可以在Releases下载.

## 使用方式
你先得有osu!和obs程序, 这些就不说了, 直接下载就可以了

1. 先给obs安装obs-websocket插件, 因为WonderfulMoments是通过这个插件来控制obs的, 它的下载方式可以在Releases中找到
1. 
    - 如果你有使用过Sync-Tools, 那么你需要到Releases中下载WonderfulMoments.zip, 然后直接丢到plugins目录里面就行
    - 如果你没听说过啥是Sync, 那你直接到Releases中下载Sync2.18.5.zip, 解压以后运行Sync.exe就可以了(打开以后你可能会看到一个类似控制台的窗口, 这个就是Sync, 关掉了窗口以后, WonderfulMoments就关了)
1. 可以用了, WonderfulMoments会帮你自动记录你的精彩时刻了, 录好的视频会放在obs录制目录里面, 正常的话只会保留你十个成绩的视频(为了节约你的硬盘空间), 可以在配置文件里面修改保留成绩的个数

##配置文件
可以直接在Sync中输入"config"来打开配置面板, 然后展开"WonderfulMoments-Setting"
- WebSocketURL和Password: 直接参照obs里面"工具-WebSockets Server Settings"里面的URL和密码就可以了, 没有验证的话密码可以留空
- FullMode: WonderfulMoments其实有两种模式, 正常默认的是PartMode, 也可以改成FullMode
    - 如果开FullMode, 最后obs只会帮你录出来一段长视频, obs会在你开始玩的时候开始录制, 一旦打完一张图以后会帮你暂停, 再开始打下一张的时候又继续, 循环往复, 最后停止录制. 这种方式只会得到一段很大很长的视频
    - 如果开PartMode(默认), obs会在你开始打图的时候开始录制, 这张图打完以后停止录制. 这种方式最后会得到很多短小的视频.
    - **注意, 现在两种模式只支持mp4格式, 因为只有mp4才能在中间暂停然后又继续**
- VideosLimit: 限制的视频文件数量, 超过这个数量的视频会由旧到新自动删除, 不然的话会占用你太多的磁盘空间了
