let storageUpdateByMsg = new LocalStorage();

@Entry(storageUpdateByMsg)
@Component
struct WidgetCard {
  /*
   * The display priority value is 1.
   */
  readonly DISPLAY_PRIORITY_ONE: number = 1;
  /*
   * The display priority value is 2.
   */
  readonly DISPLAY_PRIORITY_TOW: number = 2;
  /*
   * The flex grow value is 1.
   */
  readonly FLEX_GROW_VALUE: number = 1;
  /*
   * The width or height full percentage setting.
   */
  readonly FULL_PERCENT: string = '100%';
  /*
   * The height of image.
   */
  readonly IMAGE_HEIGHT: string = '68%';
  /*
   * The action type.
   */
  readonly ACTION_TYPE: string = 'router';
  /*
   * The ability name.
  */
  readonly ABILITY_NAME: string = 'TuanjiePlayerAbility';
  /*
   * The message.
   */
  readonly MESSAGE: string = 'add detail';
  /*
   * The layoutWeightValue.
   */
  readonly LAYOUT_WEIGHT_VALUE: number = 1;
  /*
   * The maxLinesValue.
   */
  readonly MAX_LINES_VALUE: number = 1;

  @LocalStorageProp('coins') coins: number = 0;

  @Provide('animationStatus') status: AnimationStatus = AnimationStatus.Initial

  build() {
    Stack({ alignContent: Alignment.TopStart }) {
      Flex({ direction: FlexDirection.Column }) {
        Flex({ direction: FlexDirection.Row }) {
          Column() {
            KittyAnimation()
              .width(this.IMAGE_HEIGHT)
              .height(this.IMAGE_HEIGHT)
              .margin({ bottom: $r('app.float.image_margin_bottom') })
              .flexGrow(this.FLEX_GROW_VALUE)

            Row() {
              Image($r('app.media.coin'))
                .height(24)
                .width(24)
                .objectFit(ImageFit.Contain)
                .margin({ right: 10 })
              Text(`${this.coins}`)
                .fontSize($r('app.float.card_title_font_size'))
                .width(this.FULL_PERCENT)
                .fontColor($r('app.color.information_title_font'))
                .fontWeight(FontWeight.Medium)
                .maxLines(this.MAX_LINES_VALUE)
                .textOverflow({ overflow: TextOverflow.Ellipsis })
            }
            .justifyContent(FlexAlign.Center)
            .layoutWeight(this.LAYOUT_WEIGHT_VALUE)
            .flexGrow(this.FLEX_GROW_VALUE)
          }
          .width($r('app.float.info_column_width'))
          .height(this.FULL_PERCENT)
          .flexGrow(this.FLEX_GROW_VALUE)
        }
        .backgroundColor($r('app.color.image_info_background'))
        .displayPriority(this.DISPLAY_PRIORITY_TOW)
        .flexGrow(this.FLEX_GROW_VALUE)
        .height($r('app.float.info_column_height'))
        .padding($r('app.float.info_column_padding'))
      }
    }
    .onClick(() => {
      postCardAction(this, {
        action: 'message',
        params: { msgTest: 'messageEvent' }
      })

      this.status = AnimationStatus.Running
    })
  }
}

@Component
struct KittyAnimation {
  @Consume('animationStatus') status: AnimationStatus

  build() {
    Column() {
      ImageAnimator()
        .images([
          {src: $r('app.media.1')},
          {src: $r('app.media.2')},
          {src: $r('app.media.3')},
          {src: $r('app.media.4')},
          {src: $r('app.media.5')},
        ])
        .fixedSize(true)
        .duration(700)
        .state(this.status)
        .onFinish(()=>{
          this.status = AnimationStatus.Stopped
        })
    }
  }
}