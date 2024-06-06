interface StaticSplashScreen_Params {
    showStaticSplashScreen?: boolean;
    imageFit?: ImageFit;
}
interface TuanjiePlayer_Params {
    tuanjieMainWorker?;
    webviewInfo?: WebviewInfo;
    xComponentWidth?: string;
    xComponentHeight?: string;
    orientation?: number;
    debuggerDialogInfo?: DebuggerDialogInfo;
    vAlign?: FlexAlign;
    hAlign?: FlexAlign;
    dialogController?;
}
import { TuanjieMainWorker as TuanjieMainWorker } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/workers/TuanjieMainWorker";
import { TuanjieLog } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/TuanjieLog";
import { SoftInputDialog } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/pages/SoftInputDialog";
import { VideoPlayer } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/pages/VideoPlayer";
import { GetFromGlobalThis, SetToGlobalThis } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/GlobalThisUtil";
import type resourceManager from "@ohos:resourceManager";
import { WebviewInfo, TuanjieWebview } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/pages/TuanjieWebview";
import { APP_KEY_XCOMPONENT_WIDTH, APP_KEY_XCOMPONENT_HEIGHT, APP_KEY_ORIENTATION_CHANGE } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/Constants";
import { WindowUtils } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/utils/WindowUtils";
import { DebuggerDialogInfo } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/utils/DebuggerDialogInfo";
import { DebuggerDialog } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/pages/DebuggerDialog";
import tuanjie from "@app:com.PuppetStudio.KittyFarm/entry/tuanjie";
class TuanjiePlayer extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.tuanjieMainWorker = TuanjieMainWorker.getInstance();
        this.__webviewInfo = new ObservedPropertyObjectPU(new WebviewInfo(0, 0, 0, 0), this, "webviewInfo");
        this.__xComponentWidth = this.createStorageProp(APP_KEY_XCOMPONENT_WIDTH, '0px', "xComponentWidth");
        this.__xComponentHeight = this.createStorageProp(APP_KEY_XCOMPONENT_HEIGHT, '0px', "xComponentHeight");
        this.__orientation = this.createStorageProp(APP_KEY_ORIENTATION_CHANGE, 0, "orientation");
        this.__debuggerDialogInfo = new ObservedPropertyObjectPU(new DebuggerDialogInfo("", "", "", false), this, "debuggerDialogInfo");
        this.__vAlign = new ObservedPropertySimplePU(FlexAlign.End, this, "vAlign");
        this.__hAlign = new ObservedPropertySimplePU(FlexAlign.End, this, "hAlign");
        this.dialogController = new CustomDialogController({
            builder: () => {
                let paramsLambda = () => {
                    return {
                        // showMessage: this.showMessage,
                        onTextChange: (msg: string) => {
                            TuanjieLog.debug("CustomDialogController onTextChange " + msg);
                            TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_onTextChange', data: msg });
                        },
                        onTextSelectionChange: (start: number, end: number) => {
                            TuanjieLog.debug("CustomDialogController onTextSelectionChange start: " + start.toString() + ", end: " + end.toString());
                            TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_onTextSelectionChange', start: start, length: end - start });
                        },
                        accept: (msg: string) => {
                            TuanjieLog.debug("CustomDialogController accept" + msg);
                            TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_accept', data: msg });
                        }
                    };
                };
                let jsDialog = new SoftInputDialog(this, {
                    // showMessage: this.showMessage,
                    onTextChange: (msg: string) => {
                        TuanjieLog.debug("CustomDialogController onTextChange " + msg);
                        TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_onTextChange', data: msg });
                    },
                    onTextSelectionChange: (start: number, end: number) => {
                        TuanjieLog.debug("CustomDialogController onTextSelectionChange start: " + start.toString() + ", end: " + end.toString());
                        TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_onTextSelectionChange', start: start, length: end - start });
                    },
                    accept: (msg: string) => {
                        TuanjieLog.debug("CustomDialogController accept" + msg);
                        TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_accept', data: msg });
                    },
                }, undefined, -1, paramsLambda);
                jsDialog.setController(this.dialogController);
                ViewPU.create(jsDialog);
            },
            cancel: () => {
                TuanjieLog.debug("CustomDialogController cancel");
                TuanjieMainWorker.getInstance().postMessage({ type: 'SoftInput_accept', data: null });
            },
            autoCancel: true,
            alignment: DialogAlignment.Bottom,
            customStyle: true
        }, this);
        this.setInitiallyProvidedValue(params);
        this.declareWatch("orientation", this.updateOrientation);
    }
    setInitiallyProvidedValue(params: TuanjiePlayer_Params) {
        if (params.tuanjieMainWorker !== undefined) {
            this.tuanjieMainWorker = params.tuanjieMainWorker;
        }
        if (params.webviewInfo !== undefined) {
            this.webviewInfo = params.webviewInfo;
        }
        if (params.debuggerDialogInfo !== undefined) {
            this.debuggerDialogInfo = params.debuggerDialogInfo;
        }
        if (params.vAlign !== undefined) {
            this.vAlign = params.vAlign;
        }
        if (params.hAlign !== undefined) {
            this.hAlign = params.hAlign;
        }
        if (params.dialogController !== undefined) {
            this.dialogController = params.dialogController;
        }
    }
    updateStateVars(params: TuanjiePlayer_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__webviewInfo.purgeDependencyOnElmtId(rmElmtId);
        this.__xComponentWidth.purgeDependencyOnElmtId(rmElmtId);
        this.__xComponentHeight.purgeDependencyOnElmtId(rmElmtId);
        this.__orientation.purgeDependencyOnElmtId(rmElmtId);
        this.__debuggerDialogInfo.purgeDependencyOnElmtId(rmElmtId);
        this.__vAlign.purgeDependencyOnElmtId(rmElmtId);
        this.__hAlign.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__webviewInfo.aboutToBeDeleted();
        this.__xComponentWidth.aboutToBeDeleted();
        this.__xComponentHeight.aboutToBeDeleted();
        this.__orientation.aboutToBeDeleted();
        this.__debuggerDialogInfo.aboutToBeDeleted();
        this.__vAlign.aboutToBeDeleted();
        this.__hAlign.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private tuanjieMainWorker;
    private __webviewInfo: ObservedPropertyObjectPU<WebviewInfo>;
    get webviewInfo() {
        return this.__webviewInfo.get();
    }
    set webviewInfo(newValue: WebviewInfo) {
        this.__webviewInfo.set(newValue);
    }
    private __xComponentWidth: ObservedPropertyAbstractPU<string>;
    get xComponentWidth() {
        return this.__xComponentWidth.get();
    }
    set xComponentWidth(newValue: string) {
        this.__xComponentWidth.set(newValue);
    }
    private __xComponentHeight: ObservedPropertyAbstractPU<string>;
    get xComponentHeight() {
        return this.__xComponentHeight.get();
    }
    set xComponentHeight(newValue: string) {
        this.__xComponentHeight.set(newValue);
    }
    private __orientation: ObservedPropertyAbstractPU<number>;
    get orientation() {
        return this.__orientation.get();
    }
    set orientation(newValue: number) {
        this.__orientation.set(newValue);
    }
    private __debuggerDialogInfo: ObservedPropertyObjectPU<DebuggerDialogInfo>;
    get debuggerDialogInfo() {
        return this.__debuggerDialogInfo.get();
    }
    set debuggerDialogInfo(newValue: DebuggerDialogInfo) {
        this.__debuggerDialogInfo.set(newValue);
    }
    private __vAlign: ObservedPropertySimplePU<FlexAlign>;
    get vAlign() {
        return this.__vAlign.get();
    }
    set vAlign(newValue: FlexAlign) {
        this.__vAlign.set(newValue);
    }
    private __hAlign: ObservedPropertySimplePU<FlexAlign>;
    get hAlign() {
        return this.__hAlign.get();
    }
    set hAlign(newValue: FlexAlign) {
        this.__hAlign.set(newValue);
    }
    updateOrientation() {
        // portrait upside down
        this.vAlign = this.orientation == 2 ? FlexAlign.Start : FlexAlign.End;
        // landscape right
        this.hAlign = this.orientation == 1 ? FlexAlign.Start : FlexAlign.End;
    }
    private dialogController;
    onPageShow() {
        TuanjieLog.info('%{public}s', 'onPageShow');
        this.updateOrientation();
        WindowUtils.setXComponentSizeWithSafeArea(tuanjie.nativeGetIsRenderOutsizeSafeArea());
        SetToGlobalThis('dialogController', this.dialogController);
        SetToGlobalThis('webviewInfo', this.webviewInfo);
        SetToGlobalThis(DebuggerDialogInfo.DebuggerDialogInfoKey, this.debuggerDialogInfo);
    }
    onPageHide() {
        TuanjieLog.info('%{public}s', 'onPageHide');
    }
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Row.create();
            Row.width('100%');
            Row.backgroundColor('#ff000000');
            Row.justifyContent(this.hAlign);
        }, Row);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Column.create();
            Column.height('100%');
            Column.backgroundColor('#ff000000');
            Column.justifyContent(this.vAlign);
        }, Column);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            XComponent.create({ id: 'TuanjiePlayer', type: 'surface', libraryname: 'tuanjie' }, "com.PuppetStudio.KittyFarm/entry");
            XComponent.onLoad((context) => {
                TuanjieLog.info('%{public}s', 'XComponent onLoad');
            });
            XComponent.onDestroy(() => {
                TuanjieLog.info('%{public}s', 'XComponent onDestroy');
            });
            XComponent.onAppear(() => {
                TuanjieLog.info('%{public}s', 'XComponent onAppear');
            });
            XComponent.onDisAppear(() => {
                TuanjieLog.info('%{public}s', 'XComponent onDisAppear');
            });
            XComponent.width(this.xComponentWidth);
            XComponent.height(this.xComponentHeight);
        }, XComponent);
        {
            this.observeComponentCreation2((elmtId, isInitialRender) => {
                if (isInitialRender) {
                    let paramsLambda = () => {
                        return {
                            viewInfo: this.webviewInfo
                        };
                    };
                    ViewPU.create(new TuanjieWebview(this, { viewInfo: this.__webviewInfo }, undefined, elmtId, paramsLambda));
                }
                else {
                    this.updateStateVarsOfChildByElmtId(elmtId, {});
                }
            }, null);
        }
        {
            this.observeComponentCreation2((elmtId, isInitialRender) => {
                if (isInitialRender) {
                    let paramsLambda = () => {
                        return {};
                    };
                    ViewPU.create(new StaticSplashScreen(this, {}, undefined, elmtId, paramsLambda));
                }
                else {
                    this.updateStateVarsOfChildByElmtId(elmtId, {});
                }
            }, null);
        }
        {
            this.observeComponentCreation2((elmtId, isInitialRender) => {
                if (isInitialRender) {
                    let paramsLambda = () => {
                        return {};
                    };
                    ViewPU.create(new VideoPlayer(this, {}, undefined, elmtId, paramsLambda));
                }
                else {
                    this.updateStateVarsOfChildByElmtId(elmtId, {});
                }
            }, null);
        }
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            If.create();
            if (this.debuggerDialogInfo.Showing) {
                this.ifElseBranchUpdateFunction(0, () => {
                    {
                        this.observeComponentCreation2((elmtId, isInitialRender) => {
                            if (isInitialRender) {
                                let paramsLambda = () => {
                                    return {
                                        debuggerDialogInfo: this.debuggerDialogInfo
                                    };
                                };
                                ViewPU.create(new DebuggerDialog(this, { debuggerDialogInfo: this.__debuggerDialogInfo }, undefined, elmtId, paramsLambda));
                            }
                            else {
                                this.updateStateVarsOfChildByElmtId(elmtId, {});
                            }
                        }, null);
                    }
                });
            }
            else {
                this.ifElseBranchUpdateFunction(1, () => {
                });
            }
        }, If);
        If.pop();
        Column.pop();
        Row.pop();
    }
    rerender() {
        this.updateDirtyElements();
    }
}
class StaticSplashScreen extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.__showStaticSplashScreen = new ObservedPropertySimplePU(GetFromGlobalThis('showStaticSplashScreen'), this, "showStaticSplashScreen");
        this.__imageFit = new ObservedPropertySimplePU(0, this, "imageFit");
        this.setInitiallyProvidedValue(params);
    }
    setInitiallyProvidedValue(params: StaticSplashScreen_Params) {
        if (params.showStaticSplashScreen !== undefined) {
            this.showStaticSplashScreen = params.showStaticSplashScreen;
        }
        if (params.imageFit !== undefined) {
            this.imageFit = params.imageFit;
        }
    }
    updateStateVars(params: StaticSplashScreen_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__showStaticSplashScreen.purgeDependencyOnElmtId(rmElmtId);
        this.__imageFit.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__showStaticSplashScreen.aboutToBeDeleted();
        this.__imageFit.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private __showStaticSplashScreen: ObservedPropertySimplePU<boolean>;
    get showStaticSplashScreen() {
        return this.__showStaticSplashScreen.get();
    }
    set showStaticSplashScreen(newValue: boolean) {
        this.__showStaticSplashScreen.set(newValue);
    }
    private __imageFit: ObservedPropertySimplePU<ImageFit>;
    get imageFit() {
        return this.__imageFit.get();
    }
    set imageFit(newValue: ImageFit) {
        this.__imageFit.set(newValue);
    }
    async aboutToAppear() {
        let resourceManager = getContext(this).resourceManager;
        let resConf = { "id": 16777219, "type": 10007, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" };
        let resource: resourceManager.Resource = {
            bundleName: resConf.bundleName,
            moduleName: resConf.moduleName,
            id: resConf.id
        };
        this.imageFit = await resourceManager.getNumber(resource);
        setTimeout(() => {
            this.showStaticSplashScreen = false;
        }, 2600);
    }
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Stack.create();
        }, Stack);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            If.create();
            if (this.showStaticSplashScreen) {
                this.ifElseBranchUpdateFunction(0, () => {
                    this.observeComponentCreation2((elmtId, isInitialRender) => {
                        Image.create({ "id": 16777246, "type": 20000, params: [], "bundleName": "com.PuppetStudio.KittyFarm", "moduleName": "entry" });
                        Image.objectFit(this.imageFit);
                    }, Image);
                });
            }
            else {
                this.ifElseBranchUpdateFunction(1, () => {
                });
            }
        }, If);
        If.pop();
        Stack.pop();
    }
    rerender() {
        this.updateDirtyElements();
    }
}
ViewStackProcessor.StartGetAccessRecordingFor(ViewStackProcessor.AllocateNewElmetIdForNextComponent());
loadDocument(new TuanjiePlayer(undefined, {}));
ViewStackProcessor.StopGetAccessRecording();
