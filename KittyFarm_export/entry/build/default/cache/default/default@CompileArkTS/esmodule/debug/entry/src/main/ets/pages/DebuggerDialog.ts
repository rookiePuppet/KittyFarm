interface DebuggerDialog_Params {
    debuggerDialogInfo?: DebuggerDialogInfo;
}
import promptAction from "@ohos:promptAction";
import type { DebuggerDialogInfo } from '../utils/DebuggerDialogInfo';
import tuanjie from "@app:com.PuppetStudio.KittyFarm/entry/tuanjie";
export class DebuggerDialog extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.__debuggerDialogInfo = new SynchedPropertyObjectTwoWayPU(params.debuggerDialogInfo, this, "debuggerDialogInfo");
        this.setInitiallyProvidedValue(params);
    }
    setInitiallyProvidedValue(params: DebuggerDialog_Params) {
    }
    updateStateVars(params: DebuggerDialog_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__debuggerDialogInfo.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__debuggerDialogInfo.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private __debuggerDialogInfo: SynchedPropertySimpleOneWayPU<DebuggerDialogInfo>;
    get debuggerDialogInfo() {
        return this.__debuggerDialogInfo.get();
    }
    set debuggerDialogInfo(newValue: DebuggerDialogInfo) {
        this.__debuggerDialogInfo.set(newValue);
    }
    initialRender() { }
    aboutToAppear() {
        promptAction.showDialog({
            title: this.debuggerDialogInfo.Title,
            message: this.debuggerDialogInfo.Message,
            buttons: [
                {
                    text: this.debuggerDialogInfo.buttonText,
                    color: "#000000"
                }
            ]
        }).then(data => {
            tuanjie.nativeDebuggerDialogClosed();
            this.debuggerDialogInfo.Showing = false;
        });
    }
    rerender() {
        this.updateDirtyElements();
    }
}
;
