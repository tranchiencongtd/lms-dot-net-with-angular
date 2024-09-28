import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { RealEstateComponent } from './real-estate/real-estate.component';


const routes: Routes = [
  {
    path: "",
    component: RealEstateComponent
  },
  {
    path: "real-estate",
    component: RealEstateComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardsRoutingModule { }
