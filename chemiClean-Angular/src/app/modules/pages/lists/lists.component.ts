import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EndpointService } from 'src/app/@core/services/endpoint.service';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SortDirection } from 'src/app/@core/enums/shared.enum';
import { IdDto } from 'src/app/@core/models/IdDto';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ListGetAllResponseDto } from 'src/app/@infrastructure/models/lists/outPut/Lists-Get-All-Response-Dto';
import { ListGetAllDto } from 'src/app/@infrastructure/models/lists/input/List-Get-All-Dto';
import { ListsService } from "src/app/@infrastructure/services/lists.service";

@Component({
  selector: 'app-articles',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})


export class ListsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  pageEvent: PageEvent;
  pageLength: number;

  displayedColumns: string[] = ['productName', 'supplierName',  'lastModified', 'actions',];
  dataSource = new MatTableDataSource<ListGetAllResponseDto>();

  list: ListGetAllResponseDto[];
  currentInput: IdDto;

  constructor(
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private ListsService: ListsService) { }

  async ngOnInit() {
    await this.initializeData();
  }


  Download = (rowdata: any) => {
    window.open("https://localhost:44381/api/Product/ShowPDF/GetFile/"+ rowdata.id);
  }

  openSnackBar = (message: string, action?: string) => this.snackBar.open(message, action, { duration: 2000 });

  async initializeData( ) {
    this.list = await this.ListsService.getListsAsync() as ListGetAllResponseDto[];
    this.dataSource.data = this.list;
    this.pageLength = this.list?.length;
  }

  applyFilter = (event: Event) => {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue?.trim();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngAfterViewInit = () => {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


  update(e: any) {
    this.dataSource.data = this.list?.slice(e.pageIndex * e.pageSize, (e.pageIndex + 1) * e.pageSize);
  }
}
