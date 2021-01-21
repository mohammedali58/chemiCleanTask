import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { ResponseResult } from '../models/responseresult';
import { EndpointService } from './endpoint.service';


@Injectable({ providedIn: 'root' })
export class BaseService {

  constructor(private http: HttpClient,
    private endpointService: EndpointService,
    private snackBar: MatSnackBar) { }


  public get<TResponse>(uri: string, queryString: Map<string, string> | undefined | null): Observable<TResponse> {
    var params: string = "";
    if (queryString) {
      var str = [];
      for (var p in queryString)
        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(queryString.get(p) || ''));
      params = '?' + str.join("&");
    }
    return new Observable<TResponse>((observer) => {
      this.http.get(`${this.endpointService.getConfig().ApiUrl + uri + params}`)
        .subscribe((response: ResponseResult<TResponse>) => {
          if (response.isSuccess === true) {
            return observer.next(response.data);
          }
          this.snackBar.open(response.messages?.join(','), '', {
            duration: 2000,
            panelClass: ['error-snackbar'],
          });
        });
    });
  }

  public async getAsync<TResponse>(uri: string, queryString: Map<string, string> | undefined | null): Promise<TResponse | undefined> {
    var _response: ResponseResult<TResponse>;
    var params: string = "";
    if (queryString) {
      var str = [];
      for (var p in queryString)
        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(queryString.get(p) || ''));
      params = '?' + str.join("&");
    }
    return await this.http.get(`${this.endpointService.getConfig().ApiUrl + uri + params}`)
      .toPromise()
      .then(response => {
        _response = response as ResponseResult<TResponse>;
        if (_response.isSuccess === true) {
          return _response.data;
        }
        return undefined;
      }).catch(x => {
        throw x
      });
  }

  public async getByIdAsync<TResponse>(uri: string, id: number): Promise<TResponse | undefined> {
    var _response: ResponseResult<TResponse>;
    var params: string = "";
    params = '?id=' + id;
    return await this.http.get(`${this.endpointService.getConfig().ApiUrl + uri + params}`)
      .toPromise()
      .then(response => {
        _response = response as ResponseResult<TResponse>;
        if (_response.isSuccess === true) {
          return _response.data;
        }
        return undefined;
      }).catch(x => {
        throw x
      });
  }
  public getAll<TResponse>(uri: string, queryString: Map<string, string> | undefined | null): Observable<TResponse> {
    var params: string = "";
    if (queryString) {
      var str = [];
      for (var p in queryString)
        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(queryString.get(p) || ''));
      params = '?' + str.join("&");
    }
    return new Observable<TResponse>((observer) => {
      this.http.get(`${this.endpointService.getConfig().ApiUrl + uri + params}`)
        .subscribe((response: ResponseResult<TResponse>) => {
          if (response.isSuccess === true) {
            return observer.next(response.data);
          }
        });
    });
  }

  public async getAllAsync<TResponse>(uri: string, queryString: Map<string, string> | undefined | null): Promise<TResponse[] | undefined> {
    var _response: ResponseResult<TResponse[]>;
    var params: string = "";
    if (queryString) {
      var str = [];
      for (var p in queryString)
        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(p));
      params = '?' + str.join("&");
    }

    return await this.http.get(`${this.endpointService.getConfig().ApiUrl + uri + params}`)
      .toPromise()
      .then(response => {
        _response = response as ResponseResult<TResponse[]>;
        if (_response.isSuccess === true) {
          return _response.data;
        }
        return undefined;
      }).catch(x => {
        throw x
      });
  }

  public post<TRequest, TResponse>(uri: string, model: TRequest): Observable<TResponse> {
    return new Observable<TResponse>((observer) => {
      this.http.post(`${this.endpointService.getConfig().ApiUrl + uri}`, model)
        .subscribe((response: ResponseResult<TResponse>) => {
          if (response.isSuccess === true) {
            return observer.next(response.data);
          }
        });
    });
  }

  public async postAsync<TRequest, TResponse>(uri: string, model: TRequest): Promise<TResponse | undefined> {
    var _response: ResponseResult<TResponse>;
    var url = this.endpointService.getConfig().ApiUrl + uri;
    return await this.http.post(`${this.endpointService.getConfig().ApiUrl + uri}`, model)
      .toPromise()
      .then(response => {
        _response = response as ResponseResult<TResponse>;
        if (_response.isSuccess === true) {
          return _response.data;
        }
        this.snackBar.open(_response.messages?.join(','), '', {
          duration: 2000,
          panelClass: ['error-snackbar'],
        });
        return undefined;
      }).catch(x => {
        throw x;
      });
  }

  public put<TRequest, TResponse>(uri: string, model: TRequest): Observable<TResponse> {
    return new Observable<TResponse>((observer) => {
      this.http.put(`${this.endpointService.getConfig().ApiUrl + uri}`, model)
        .subscribe((response: ResponseResult<TResponse>) => {
          if (response.isSuccess === true) {
            return observer.next(response.data);
          }
        });
    });
  }

  public async putAsync<TRequest, TResponse>(uri: string, model: TRequest): Promise<TResponse | undefined> {
    var _response: ResponseResult<TResponse>;
    return await this.http.put(`${this.endpointService.getConfig().ApiUrl + uri}`, model)
      .toPromise()
      .then(response => {
        _response = response as ResponseResult<TResponse>;
        if (_response.isSuccess === true) {
          return _response.data;
        }
        return undefined;
      }).catch(x => {
        throw x
      });
  }

  public delete<TRequest, TResponse>(uri: string, model: TRequest): Observable<TResponse> {
    return new Observable<TResponse>((observer) => {
      this.http.get(`${this.endpointService.getConfig().ApiUrl + uri}`)
        .subscribe((response: ResponseResult<TResponse>) => {
          if (response.isSuccess === true) {
            return observer.next(response.data);
          }
        });
    });
  }

  public async deleteAsync<TRequest, TResponse>(uri: string, model: TRequest): Promise<TResponse | undefined> {
    var _response: ResponseResult<TResponse>;
    return await this.http.delete(`${this.endpointService.getConfig().ApiUrl + uri}`)
      .toPromise()
      .then(response => {
        _response = response as ResponseResult<TResponse>;
        if (_response.isSuccess === true) {
          return _response.data;
        }
        return undefined;
      }).catch(x => {
        throw x
      });
  }


}
