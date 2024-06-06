interface SoftInputDialog_Params {
    showMessage?: string;
    inputMessage?: string;
    onTextChange?: (msg: string) => void;
    onTextSelectionChange?: (start: number, end: number) => void;
    accept?: (msg: string) => void;
    controller?: CustomDialogController;
    cancel?: () => void;
    confirm?: () => void;
}
import { GetFromGlobalThis } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/GlobalThisUtil";
export class SoftInputDialog extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.showMessage = '';
        this.inputMessage = '';
        this.onTextChange = () => {
        };
        this.onTextSelectionChange = () => {
        };
        this.accept = () => {
        };
        this.controller = undefined;
        this.cancel = () => {
        };
        this.confirm = () => {
        };
        this.setInitiallyProvidedValue(params);
    }
    setInitiallyProvidedValue(params: SoftInputDialog_Params) {
        if (params.showMessage !== undefined) {
            this.showMessage = params.showMessage;
        }
        if (params.inputMessage !== undefined) {
            this.inputMessage = params.inputMessage;
        }
        if (params.onTextChange !== undefined) {
            this.onTextChange = params.onTextChange;
        }
        if (params.onTextSelectionChange !== undefined) {
            this.onTextSelectionChange = params.onTextSelectionChange;
        }
        if (params.accept !== undefined) {
            this.accept = params.accept;
        }
        if (params.controller !== undefined) {
            this.controller = params.controller;
        }
        if (params.cancel !== undefined) {
            this.cancel = params.cancel;
        }
        if (params.confirm !== undefined) {
            this.confirm = params.confirm;
        }
    }
    updateStateVars(params: SoftInputDialog_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
    }
    aboutToBeDeleted() {
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private showMessage: string;
    private inputMessage: string;
    private onTextChange: (msg: string) => void;
    private onTextSelectionChange: (start: number, end: number) => void;
    private accept: (msg: string) => void;
    private controller?: CustomDialogController;
    setController(ctr: CustomDialogController) {
        this.controller = ctr;
    }
    private cancel: () => void;
    private confirm: () => void;
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Column.create();
            Column.width('100%');
            Column.justifyContent(FlexAlign.End);
        }, Column);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Row.create();
            Row.padding({ left: 8, right: 8, top: 8, bottom: 8 });
            Row.backgroundColor(Color.Gray);
        }, Row);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            TextInput.create({ text: GetFromGlobalThis('inputInitialText') });
            TextInput.backgroundColor('#ffffff');
            TextInput.layoutWeight(1);
            TextInput.onChange((value) => {
                if (this.onTextChange) {
                    this.onTextChange(value);
                }
                this.inputMessage = value;
            });
            TextInput.onTextSelectionChange((start, end) => {
                if (this.onTextSelectionChange) {
                    this.onTextSelectionChange(start, end);
                }
            });
            TextInput.onSubmit((value) => {
                if (this.accept) {
                    this.accept(this.inputMessage);
                }
                this.controller!.close();
            });
        }, TextInput);
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Blank.create(8);
            Blank.width(16);
        }, Blank);
        Blank.pop();
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            Button.createWithLabel('完成');
            Button.onClick(() => {
                if (this.accept) {
                    this.accept(this.inputMessage);
                }
                this.controller!.close();
            });
        }, Button);
        Button.pop();
        Row.pop();
        Column.pop();
    }
    rerender() {
        this.updateDirtyElements();
    }
}
