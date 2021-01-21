import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ListsComponent } from './lists/lists.component';
import { pagesComponent } from './pages.component';


const routes: Routes = [

  {
    path: 'pages',
    component: pagesComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      },
      {
        path: 'home',
        component: IndexComponent
      }
      ,
      {
        path: 'list',
        component: ListsComponent
      }
      // ,
      // {
      //   path: 'newsDetails/:id',
      //   component: NewsdetailsComponent
      // }
      
    ],
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class pagesRoutingModule { }
