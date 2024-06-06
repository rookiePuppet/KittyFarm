interface VideoPlayer_Params {
    videoInfo?: VideoInfo;
    controller?: VideoController;
    url?: string;
    bgColor?: number;
    controlShow?: boolean;
    scalingMode?: ImageFit;
}
import { VideoInfo } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/utils/VideoPlayerProxy";
import { VideoPlayerProxy } from "@bundle:com.PuppetStudio.KittyFarm/entry/ets/utils/VideoPlayerProxy";
export class VideoPlayer extends ViewPU {
    constructor(parent, params, __localStorage, elmtId = -1, paramsLambda = undefined) {
        super(parent, __localStorage, elmtId);
        if (typeof paramsLambda === "function") {
            this.paramsGenerator_ = paramsLambda;
        }
        this.__videoInfo = this.createStorageLink('VideoInfo', new VideoInfo(), "videoInfo");
        this.controller = new VideoController();
        this.url = '';
        this.bgColor = 0;
        this.controlShow = true;
        this.scalingMode = ImageFit.Contain;
        this.setInitiallyProvidedValue(params);
        this.declareWatch("videoInfo", this.onVideoInfoUpdated);
    }
    setInitiallyProvidedValue(params: VideoPlayer_Params) {
        if (params.controller !== undefined) {
            this.controller = params.controller;
        }
        if (params.url !== undefined) {
            this.url = params.url;
        }
        if (params.bgColor !== undefined) {
            this.bgColor = params.bgColor;
        }
        if (params.controlShow !== undefined) {
            this.controlShow = params.controlShow;
        }
        if (params.scalingMode !== undefined) {
            this.scalingMode = params.scalingMode;
        }
    }
    updateStateVars(params: VideoPlayer_Params) {
    }
    purgeVariableDependenciesOnElmtId(rmElmtId) {
        this.__videoInfo.purgeDependencyOnElmtId(rmElmtId);
    }
    aboutToBeDeleted() {
        this.__videoInfo.aboutToBeDeleted();
        SubscriberManager.Get().delete(this.id__());
        this.aboutToBeDeletedInternal();
    }
    private __videoInfo: ObservedPropertyAbstractPU<VideoInfo>;
    get videoInfo() {
        return this.__videoInfo.get();
    }
    set videoInfo(newValue: VideoInfo) {
        this.__videoInfo.set(newValue);
    }
    private controller: VideoController;
    private url: string;
    private bgColor: number;
    private controlShow: boolean;
    private scalingMode: ImageFit;
    onVideoInfoUpdated(propName: string): void {
        this.url = this.videoInfo.url;
        this.bgColor = this.videoInfo.bgColor;
        this.controlShow = this.videoInfo.controlMode < 2;
        switch (this.videoInfo.scalingMode) {
            case 0:
                this.scalingMode = ImageFit.None;
                break;
            case 1:
                this.scalingMode = ImageFit.Contain;
                break;
            case 2:
                this.scalingMode = ImageFit.Cover;
                break;
            case 3:
                this.scalingMode = ImageFit.Fill;
                break;
        }
    }
    start(): void {
        VideoPlayerProxy.OnVideoStart();
    }
    stop(): void {
        this.videoInfo.isPlaying = false;
        VideoPlayerProxy.OnVideoStop();
    }
    initialRender() {
        this.observeComponentCreation2((elmtId, isInitialRender) => {
            If.create();
            if (this.videoInfo.isPlaying) {
                this.ifElseBranchUpdateFunction(0, () => {
                    this.observeComponentCreation2((elmtId, isInitialRender) => {
                        Stack.create();
                    }, Stack);
                    this.observeComponentCreation2((elmtId, isInitialRender) => {
                        Video.create({
                            src: this.url,
                            controller: this.controller
                        });
                        Video.controls(this.controlShow);
                        Video.autoPlay(true);
                        Video.objectFit(this.scalingMode);
                        Video.loop(false);
                        Video.onStart(() => {
                            this.start();
                        });
                        Video.onFinish(() => {
                            this.stop();
                        });
                        Video.onError(() => {
                            this.stop();
                        });
                        Video.backgroundColor(this.bgColor);
                        Video.onClick((event: ClickEvent) => {
                            if (this.videoInfo.controlMode == 2) {
                                this.stop();
                            }
                        });
                    }, Video);
                    Stack.pop();
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
