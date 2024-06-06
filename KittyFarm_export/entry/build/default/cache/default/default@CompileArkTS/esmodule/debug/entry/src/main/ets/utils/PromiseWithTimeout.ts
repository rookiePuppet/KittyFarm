import tuanjie from "@app:com.PuppetStudio.KittyFarm/entry/tuanjie";
export class PromiseWithTimeout {
    #userData: number;
    #timeoutMs: number;
    constructor(userData: number, timeoutMs: number) {
        this.#userData = userData;
        this.#timeoutMs = timeoutMs;
    }
    async executePromise(asyncPromise, transferFunc: Function) {
        const timeoutPromise = new Promise((resolve, reject) => {
            setTimeout(() => reject(new Error('Async call timeout limit reached')), this.#timeoutMs);
        });
        // return a promise without reject
        return Promise.race([asyncPromise, timeoutPromise]).then(result => {
            let finalResult = transferFunc(result);
            tuanjie.nativeJSAsyncCallResult(true, this.#userData, finalResult);
        }).catch(reason => {
            tuanjie.nativeJSAsyncCallResult(false, this.#userData, undefined);
        });
    }
}
