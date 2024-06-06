interface TuanjieWebview_Params {
    viewInfo?: WebviewInfo;
}
import web_webview from "@ohos:web.webview";
export class WebviewInfo {
    // position
    public x: number = 0;
    public y: number = 0;
    // size
    public w: number = 0;
    public h: number = 0;
    // url
    public url: string = '';
    public visible: boolean = false;
    public occupyble: boolean = false;
    public controller: web_webview.WebviewController = new web_webview.WebviewController();
    constructor(x: number, y: number, w: number, h: number) {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
    public reset() {
        this.x = 0;
        this.y = 0;
        this.w = 0;
        this.h = 0;
        this.url = '';
        this.visible = false;
        this.occupyble = false;
    }
}
export class TuanjieWebview extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.__viewInfo = new SynchedPropertyObjectTwoWayPU(params.viewInfo, this, "viewInfo");
        this.setInitiallyProvidedValue(params);
    }
    setInitiallyProvidedValue(params: TuanjieWebview_Params) {
    }
    updateStateVars(params: TuanjieWebview_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__viewInfo.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__viewInfo.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private __viewInfo: SynchedPropertySimpleOneWayPU<WebviewInfo>;
    get viewInfo() {
        return this.__viewInfo.get();
    }
    set viewInfo(newValue: WebviewInfo) {
        this.__viewInfo.set(newValue);
    }
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            If.create();
            if (this.viewInfo.visible) {
                this.ifElseBranchUpdateFunction(0, () => {
                    this.observeComponentCreation2((elmtId, isInitialRender) => {
                        Web.create({
                            src: this.viewInfo.url,
                            controller: this.viewInfo.controller
                        });
                        Web.position({ x: px2vp(this.viewInfo.x), y: px2vp(this.viewInfo.y) });
                        Web.width(px2vp(this.viewInfo.w));
                        Web.height(px2vp(this.viewInfo.h));
                        Web.border({ width: 1 });
                        Web.domStorageAccess(true);
                        Web.databaseAccess(true);
                        Web.imageAccess(true);
                        Web.javaScriptAccess(true);
                        Web.visibility(this.viewInfo.occupyble ? (this.viewInfo.visible ? Visibility.Visible : Visibility.Hidden) : Visibility.None);
                    }, Web);
                });
            }
            else {
                this.ifElseBranchUpdateFunction(1, () => {
                });
            }
        }, If);
        If.pop();
    }
    rerender() {
        this.updateDirtyElements();
    }
}
