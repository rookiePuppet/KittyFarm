import { GetFromGlobalThis } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/common/GlobalThisUtil";
import type { Context } from "@ohos:abilityAccessCtrl";
import util from "@ohos:util";
import dataPreferences from "@ohos:data.preferences";
export class TuanjieAAID {
    static async GetAAID(): Promise<any> {
        // let id = await AAID.getAAID();
        // if (id) {
        //   return id;
        // }
        let context: Context = GetFromGlobalThis('AbilityContext');
        let options: dataPreferences.Options = { name: 'tuanjieStore' };
        let preferences = dataPreferences.getPreferencesSync(context, options);
        let cachedKey = "tuanjie-cached-random-uuid";
        if (preferences.hasSync(cachedKey)) {
            return preferences.getSync(cachedKey, '');
        }
        let uuid = util.generateRandomUUID(true);
        preferences.putSync(cachedKey, uuid);
        preferences.flush((err) => {
            if (err) {
                console.error("Failed to flush. code =" + err);
                return;
            }
        });
        return uuid;
    }
}
