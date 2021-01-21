import { Injectable } from '@angular/core';

export interface AppConfig {
  ApiUrl: string;
  JobYearsCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class EndpointService<T extends AppConfig = AppConfig> {
  public static appConfig: AppConfig;

  constructor() { }

  static loadConfig(): Promise<void> {
    return new Promise((resolve, reject) => {
      const oReq = new XMLHttpRequest();
      oReq.addEventListener('load', (resp) => {
        if (oReq.status === 200) {
          try {
            EndpointService.appConfig = JSON.parse(oReq.responseText);
          } catch (e) {
            reject(e);
          }
          resolve();
        } else {
          reject(oReq.statusText);
        }
      });
      
      oReq.open('GET', './assets/settings/app.config.json');
      oReq.send();
    });
  }

  getConfig(): T {
    return EndpointService.appConfig as T;
  }

}
