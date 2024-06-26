## 项目概述

项目名称：猫咪农场（KittyFarm）

团队名称：海底星辰

团队成员：胡建波

### 游戏介绍

猫咪农场是一款2D模拟经营游戏，具有可爱的像素美术风格。

在游戏中你将化身为一只小猫咪，为了逃离尘世的喧嚣，独自前往荒芜的小岛上开辟一片属于自己的天地。

岛上似乎有其他人生活过的痕迹——一间不大的别墅、两块荒废的农田和可交易的无人商店，感觉一切都是为你准备好的。

“接下来就可以自由地种菜了！”你欢喜地说道。

### 开发工具相关

团结引擎：1.0.4

Rider：2023.3.3

OpenHarmony版本：4.0


## 游戏说明

### 主要玩法

使用锄头为耕地松土，撒上在商店购买的种子，静待作物成熟并收获，卖出作物获得金币，如此循环。

### 详细功能

#### 1.背包与物品系统

背包中最多存放9种不同的物品，拖动物品可以交换位置。

![](ReadmePictures/inventory_item_swap.gif)

接触到掉落在地图上的物品时，可将其拾取并放入背包中。

![](ReadmePictures/item_pickup.gif)

选中物品后，点击地图可以进行使用。选中锄头点击耕地可以松土，选中种子点击松过土的耕地可以种植，选中收获的作物或资源可以将其丢弃。

![](ReadmePictures/item_use.gif)

#### 2.作物种植系统

种下种子后，作物会随时间不断生长。每一种作物均有多个阶段，成熟所花费的时间各不相同。

作物成熟后，点击可进行收获，收获数量在配置的范围内随机。

![](ReadmePictures/crop_growth_harvest.gif)

作物在生长时，点击可查看其生长的具体信息。

![](ReadmePictures/check_crop_info.gif)

#### 3.资源重复采集

地图上有各种资源可点击进行收集，如苹果树、梨树等。

资源采集后过一段时间会重新生成。

![](ReadmePictures/resource_gather.gif)

#### 4.商店

在商店窗口中，可以查看商品的详细信息，输入购买数量后点击购买。

将背包物品拖入商店，可将物品卖出，获得金币。

![](ReadmePictures/shop.gif)

#### 5.环境交互

当小猫站在树木和巨大的石头后面时，物体的不透明度会降低。

![](ReadmePictures/environment.gif)

#### 6.昼夜光照变化

游戏时间与现实时间同步，场景中的光照会随时间不断变化，实现昼夜更替的效果。

#### 7.音乐与音效

游戏包含一首背景音乐，和多种音效，可以在设置窗口中进行开关和音量调节。

### 美术与音乐

本项目使用到的所有美术资源来自Cup Nooble，本人已购买获得许可。

1. [Sprout Lands Asset Pack](https://cupnooble.itch.io/sprout-lands-asset-pack)
2. [Sprout Lands UI Pack](https://cupnooble.itch.io/sprout-lands-ui-pack)

背景音乐名为 Keep Smiling，来自[FITTYSOUNDS](https://www.fiftysounds.com/)。

音效资源来自Unity Asset Store，名为[FREE Casual Game SFX Pack](https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116)。

## 应用安装包

使用团结引擎导出的OpenHarmony安装包见根目录文件`kittyfarm.hap`。

## 游戏演示

[演示视频传送门](https://www.bilibili.com/video/BV1dy411Y796/)
