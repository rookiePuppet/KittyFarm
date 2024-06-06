interface KittyAnimation_Params {
    status?: AnimationStatus;
}
interface WidgetCard_Params {
    DISPLAY_PRIORITY_ONE?: number;
    DISPLAY_PRIORITY_TOW?: number;
    FLEX_GROW_VALUE?: number;
    FULL_PERCENT?: string;
    IMAGE_HEIGHT?: string;
    ACTION_TYPE?: string;
    ABILITY_NAME?: string;
    MESSAGE?: string;
    LAYOUT_WEIGHT_VALUE?: number;
    MAX_LINES_VALUE?: number;
    coins?: number;
    status?: AnimationStatus;
}
let storageUpdateByMsg = new LocalStorage();
class WidgetCard extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.DISPLAY_PRIORITY_ONE = 1;
        this.DISPLAY_PRIORITY_TOW = 2;
        this.FLEX_GROW_VALUE = 1;
        this.FULL_PERCENT = '100%';
        this.IMAGE_HEIGHT = '68%';
        this.ACTION_TYPE = 'router';
        this.ABILITY_NAME = 'TuanjiePlayerAbility';
        this.MESSAGE = 'add detail';
        this.LAYOUT_WEIGHT_VALUE = 1;
        this.MAX_LINES_VALUE = 1;
        this.__status = new ObservedPropertySimplePU(AnimationStatus.Initial, this, "status");
        this.addProvidedVar("animationStatus", this.__status);
        this.addProvidedVar("status", this.__status);
        this.setInitiallyProvidedValue(params);
    }
    setInitiallyProvidedValue(params: WidgetCard_Params) {
        if (params.DISPLAY_PRIORITY_ONE !== undefined) {
            this.DISPLAY_PRIORITY_ONE = params.DISPLAY_PRIORITY_ONE;
        }
        if (params.DISPLAY_PRIORITY_TOW !== undefined) {
            this.DISPLAY_PRIORITY_TOW = params.DISPLAY_PRIORITY_TOW;
        }
        if (params.FLEX_GROW_VALUE !== undefined) {
            this.FLEX_GROW_VALUE = params.FLEX_GROW_VALUE;
        }
        if (params.FULL_PERCENT !== undefined) {
            this.FULL_PERCENT = params.FULL_PERCENT;
        }
        if (params.IMAGE_HEIGHT !== undefined) {
            this.IMAGE_HEIGHT = params.IMAGE_HEIGHT;
        }
        if (params.ACTION_TYPE !== undefined) {
            this.ACTION_TYPE = params.ACTION_TYPE;
        }
        if (params.ABILITY_NAME !== undefined) {
            this.ABILITY_NAME = params.ABILITY_NAME;
        }
        if (params.MESSAGE !== undefined) {
            this.MESSAGE = params.MESSAGE;
        }
        if (params.LAYOUT_WEIGHT_VALUE !== undefined) {
            this.LAYOUT_WEIGHT_VALUE = params.LAYOUT_WEIGHT_VALUE;
        }
        if (params.MAX_LINES_VALUE !== undefined) {
            this.MAX_LINES_VALUE = params.MAX_LINES_VALUE;
        }
        if (params.status !== undefined) {
            this.status = params.status;
        }
    }
    updateStateVars(params: WidgetCard_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__coins.purgeDependencyOnElmtId(rmElmtId);
        this.__status.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__coins.aboutToBeDeleted();
        this.__status.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    /*
     * The display priority value is 1.
     */
    readonly DISPLAY_PRIORITY_ONE: number;
    /*
     * The display priority value is 2.
     */
    readonly DISPLAY_PRIORITY_TOW: number;
    /*
     * The flex grow value is 1.
     */
    readonly FLEX_GROW_VALUE: number;
    /*
     * The width or height full percentage setting.
     */
    readonly FULL_PERCENT: string;
    /*
     * The height of image.
     */
    readonly IMAGE_HEIGHT: string;
    /*
     * The action type.
     */
    readonly ACTION_TYPE: string;
    /*
     * The ability name.
    */
    readonly ABILITY_NAME: string;
    /*
     * The message.
     */
    readonly MESSAGE: string;
    /*
     * The layoutWeightValue.
     */
    readonly LAYOUT_WEIGHT_VALUE: number;
    /*
     * The maxLinesValue.
     */
    readonly MAX_LINES_VALUE: number;
    private __coins: ObservedPropertyAbstractPU<number> = this.createLocalStorageProp<number>("coins", 0, "coins");
    get coins() {
        return this.__coins.get();
    }
    set coins(newValue: number) {
        this.__coins.set(newValue);
    }
    private __status: ObservedPropertySimplePU<AnimationStatus>;
    get status() {
        return this.__status.get();
    }
    set status(newValue: AnimationStatus) {
        this.__status.set(newValue);
    }
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Stack.create({ alignContent: Alignment.TopStart });
            Stack.debugLine("widget/pages/WidgetCard.ets(52:5)");
            Stack.onClick(() => {
                postCardAction(this, {
                    action: 'message',
                    params: { msgTest: 'messageEvent' }
                });
                this.status = AnimationStatus.Running;
            });
        }, Stack);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Flex.create({ direction: FlexDirection.Column });
            Flex.debugLine("widget/pages/WidgetCard.ets(53:7)");
        }, Flex);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Flex.create({ direction: FlexDirection.Row });
            Flex.debugLine("widget/pages/WidgetCard.ets(54:9)");
            Flex.backgroundColor({ "id": 16777219, "type": 10001, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
            Flex.displayPriority(this.DISPLAY_PRIORITY_TOW);
            Flex.flexGrow(this.FLEX_GROW_VALUE);
            Flex.height({ "id": 16777240, "type": 10002, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
            Flex.padding({ "id": 16777241, "type": 10002, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
        }, Flex);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Column.create();
            Column.debugLine("widget/pages/WidgetCard.ets(55:11)");
            Column.width({ "id": 16777242, "type": 10002, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
            Column.height(this.FULL_PERCENT);
            Column.flexGrow(this.FLEX_GROW_VALUE);
        }, Column);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            __Common__.create();
            __Common__.width(this.IMAGE_HEIGHT);
            __Common__.height(this.IMAGE_HEIGHT);
            __Common__.margin({ bottom: { "id": 16777239, "type": 10002, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" } });
            __Common__.flexGrow(this.FLEX_GROW_VALUE);
        }, __Common__);
        {
            this.observeComponentCreation2((elmtId, isInitialRender) => {
                if (isInitialRender) {
                    let paramsLambda = () => {
                        return {};
                    };
                    ViewPU.create(new KittyAnimation(this, {}, undefined, elmtId, paramsLambda));
                }
                else {
                    this.updateStateVarsOfChildByElmtId(elmtId, {});
                }
            }, null);
        }
        __Common__.pop();
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Row.create();
            Row.debugLine("widget/pages/WidgetCard.ets(62:13)");
            Row.justifyContent(FlexAlign.Center);
            Row.layoutWeight(this.LAYOUT_WEIGHT_VALUE);
            Row.flexGrow(this.FLEX_GROW_VALUE);
        }, Row);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Image.create({ "id": 16777246, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
            Image.debugLine("widget/pages/WidgetCard.ets(63:15)");
            Image.height(24);
            Image.width(24);
            Image.objectFit(ImageFit.Contain);
            Image.margin({ right: 10 });
        }, Image);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Text.create(`${this.coins}`);
            Text.debugLine("widget/pages/WidgetCard.ets(68:15)");
            Text.fontSize({ "id": 16777237, "type": 10002, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
            Text.width(this.FULL_PERCENT);
            Text.fontColor({ "id": 16777220, "type": 10001, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
            Text.fontWeight(FontWeight.Medium);
            Text.maxLines(this.MAX_LINES_VALUE);
            Text.textOverflow({ overflow: TextOverflow.Ellipsis });
        }, Text);
        Text.pop();
        Row.pop();
        Column.pop();
        Flex.pop();
        Flex.pop();
        Stack.pop();
    }
    rerender() {
        this.updateDirtyElements();
    }
}
class KittyAnimation extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.__status = this.initializeConsume("animationStatus", "status");
        this.setInitiallyProvidedValue(params);
    }
    setInitiallyProvidedValue(params: KittyAnimation_Params) {
    }
    updateStateVars(params: KittyAnimation_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__status.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__status.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private __status: ObservedPropertyAbstractPU<AnimationStatus>;
    get status() {
        return this.__status.get();
    }
    set status(newValue: AnimationStatus) {
        this.__status.set(newValue);
    }
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Column.create();
            Column.debugLine("widget/pages/WidgetCard.ets(107:5)");
        }, Column);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            ImageAnimator.create();
            ImageAnimator.debugLine("widget/pages/WidgetCard.ets(108:7)");
            ImageAnimator.images([
                { src: { "id": 16777249, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" } },
                { src: { "id": 16777250, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" } },
                { src: { "id": 16777248, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" } },
                { src: { "id": 16777247, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" } },
                { src: { "id": 16777251, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" } },
            ]);
            ImageAnimator.fixedSize(true);
            ImageAnimator.duration(700);
            ImageAnimator.state(this.status);
            ImageAnimator.onFinish(() => {
                this.status = AnimationStatus.Stopped;
            });
        }, ImageAnimator);
        Column.pop();
    }
    rerender() {
        this.updateDirtyElements();
    }
}
ViewStackProcessor.StartGetAccessRecordingFor(ViewStackProcessor.AllocateNewElmetIdForNextComponent());
loadEtsCard(new WidgetCard(undefined, {}, storageUpdateByMsg), "com.PuppetStudio.KittyFarm/entry/ets/widget/pages/WidgetCard");
ViewStackProcessor.StopGetAccessRecording();
