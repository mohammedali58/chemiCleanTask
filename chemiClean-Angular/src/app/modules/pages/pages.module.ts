import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from "@angular/common/http";
import { pagesComponent } from './pages.component' 
import { pagesRoutingModule } from './pages-routing.module';
import { ThemeModule } from 'src/app/theme/theme.module';
import { HeaderComponent } from 'src/app/theme/Layout/header/header.component';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatStepperModule } from "@angular/material/stepper";
import { FormGroupDirective } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { IndexComponent } from './index/index.component';
import { ListsComponent } from './lists/lists.component';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';

const MATCOMPONENTS = [
  MatCardModule,
  MatTabsModule,
  MatTableModule,
  MatCheckboxModule,
  MatRadioModule,
  MatDialogModule,
  MatTooltipModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatPaginatorModule,
  MatRadioModule,
  MatSortModule,
];

@NgModule({
  declarations: [
    IndexComponent,
    ListsComponent,
    pagesComponent,
    HeaderComponent
    
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ThemeModule,
    pagesRoutingModule,
    ...MATCOMPONENTS,    
  ],
  providers: [FormGroupDirective],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class pagesModule { }
