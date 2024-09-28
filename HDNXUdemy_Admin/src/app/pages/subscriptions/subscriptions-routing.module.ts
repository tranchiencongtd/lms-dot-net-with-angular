import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { OverviewComponent } from './overview/overview.component';
import { ListPromotionsComponent } from './list-subscriptions/list-promotion.component';
import { ListCouponsComponent } from './list-coupons/list-coupons.component';

const routes: Routes = [
  {
    path: 'list-promotion',
    component: ListPromotionsComponent
  },
  {
    path: 'overview',
    component: OverviewComponent
  },
  {
    path: 'list-coupons',
    component: ListCouponsComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SubscriptionsRoutingModule { }
