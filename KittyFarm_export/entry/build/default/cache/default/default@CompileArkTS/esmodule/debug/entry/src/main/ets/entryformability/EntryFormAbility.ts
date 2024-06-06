import formInfo from "@ohos:app.form.formInfo";
import formBindingData from "@ohos:app.form.formBindingData";
import FormExtensionAbility from "@ohos:app.form.FormExtensionAbility";
import type Want from "@ohos:app.ability.Want";
import dataPreferences from "@ohos:data.preferences";
import type Base from "@ohos:base";
import formProvider from "@ohos:app.form.formProvider";
import hilog from "@ohos:hilog";
const TAG: string = 'EntryFormAbility';
const DOMAIN_NUMBER: number = 0xFF00;
class FormDataClass {
    coins: number;
    constructor(coins: number) {
        this.coins = coins;
    }
}
export default class EntryFormAbility extends FormExtensionAbility {
    onAddForm(want: Want) {
        // Called to return a FormBindingData object.
        let options: dataPreferences.Options = { name: 'game' };
        let preferences = dataPreferences.getPreferencesSync(this.context, options);
        let coins = Number(preferences.getSync("coins", 0));
        let formData = new FormDataClass(coins);
        return formBindingData.createFormBindingData(formData);
    }
    onCastToNormalForm(formId: string) {
        // Called when the form provider is notified that a temporary form is successfully
        // converted to a normal form.
    }
    onUpdateForm(formId: string) {
        // Called to notify the form provider to update a specified form.
        let options: dataPreferences.Options = { name: 'game' };
        let preferences = dataPreferences.getPreferencesSync(this.context, options);
        let coins = Number(preferences.getSync("coins", 0));
        let formData = new FormDataClass(coins);
        return formBindingData.createFormBindingData(formData);
    }
    onChangeFormVisibility(newStatus: Record<string, number>) {
        // Called when the form provider receives form events from the system.
    }
    onFormEvent(formId: string, message: string) {
        // Called when a specified message event defined by the form provider is triggered.
        hilog.info(DOMAIN_NUMBER, TAG, `FormAbility onFormEvent, formId = ${formId}, message: ${JSON.stringify(message)}`);
        let options: dataPreferences.Options = { name: 'game' };
        let preferences = dataPreferences.getPreferencesSync(this.context, options);
        let coins = Number(preferences.getSync("coins", 0));
        let formData = new FormDataClass(coins);
        let formInfo: formBindingData.FormBindingData = formBindingData.createFormBindingData(formData);
        formProvider.updateForm(formId, formInfo).then(() => {
            hilog.info(DOMAIN_NUMBER, TAG, 'FormAbility updateForm success.');
        }).catch((error: Base.BusinessError) => {
            hilog.info(DOMAIN_NUMBER, TAG, `Operation updateForm failed. Cause: ${JSON.stringify(error)}`);
        });
    }
    onRemoveForm(formId: string) {
        // Called to notify the form provider that a specified form has been destroyed.
    }
    onAcquireFormState(want: Want) {
        // Called to return a {@link FormState} object.
        return formInfo.FormState.READY;
    }
}
;
