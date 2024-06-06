import dataPreferences from "@ohos:data.preferences";
import type { Context } from "@ohos:abilityAccessCtrl";
export class OHData {
    private preferences: dataPreferences.Preferences;
    private getPreferences() {
        if (this.preferences == null) {
            let options: dataPreferences.Options = { name: 'game' };
            this.preferences = dataPreferences.getPreferencesSync(globalThis.context, options);
        }
        return this.preferences;
    }
    private getPreferencesWith(context: Context) {
        let options: dataPreferences.Options = { name: 'game' };
        return dataPreferences.getPreferencesSync(context, options);
    }
    saveCoins(amount: number) {
        var preferences = this.getPreferences();
        preferences.putSync("coins", amount);
        preferences.flush();
    }
    readCoins(context: Context) {
        let val: number = Number(this.getPreferencesWith(context).getSync("coins", 0));
        return val;
    }
}
export function RegisterOHData() {
    var register = {};
    register["OHData"] = new OHData();
    return register;
}
