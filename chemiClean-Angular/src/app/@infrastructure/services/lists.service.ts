import { Injector } from '@angular/core';
import { Injectable } from '@angular/core';
import { IdDto } from 'src/app/@core/models/IdDto';
import { BaseService } from 'src/app/@core/services/base.service';
import { ListAddRequestDto } from '../models/lists/input/List-Add-Request-Dto';
import { ListGetAllResponseDto } from '../models/lists/outPut/Lists-Get-All-Response-Dto';


@Injectable({
  providedIn: 'root'
})
export class ListsService {

  controllerName = '/api/Product/';

  constructor(private readonly injector: Injector) { }

  private baseService: BaseService = this.injector.get<BaseService>(
    BaseService
  );

  async getListsAsync( ): Promise<ListGetAllResponseDto[]> {
    let releaseDto: ListGetAllResponseDto[] | undefined;
    const getReleasesUri = this.controllerName + 'GetAll';
    releaseDto = await this.baseService.getAsync<ListGetAllResponseDto[]>(getReleasesUri, null);
    console.log(releaseDto);
    
    return releaseDto;
  }


  

  async addListAsync(addArticleDto: ListAddRequestDto): Promise<boolean | undefined> {
    let releaseDto: boolean | undefined;
    const getReleaseUri = `${this.controllerName}Add`;
    releaseDto = await this.baseService.postAsync<ListAddRequestDto, boolean>(getReleaseUri, addArticleDto);
    return releaseDto;
  }



}





