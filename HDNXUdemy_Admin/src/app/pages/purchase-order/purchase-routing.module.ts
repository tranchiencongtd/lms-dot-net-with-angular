import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListPurchaseOrderComponent } from './list-purchase-order/list-purchase-order.component';
import { PurchaseOrderReviewComponent } from './purchase-review/purchase-review.component';

// Component

const routes: Routes = [
  {
    path: 'list-purchase-order',
    component: ListPurchaseOrderComponent
  },
  {
    path: 'purchase-order-review/:id',
    component: PurchaseOrderReviewComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchaseOrderRoutingModule { }
