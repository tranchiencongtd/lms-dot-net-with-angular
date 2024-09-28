import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { ListNewsComponent } from './list-news/list-news.component';
import { ProductsGridComponent } from './products-grid/products-grid.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { AddProductComponent } from './add-product/add-product.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderOverviewComponent } from './order-overview/order-overview.component';
import { CustomersComponent } from './customers/customers.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { SellersComponent } from './sellers/sellers.component';
import { SellerOverviewComponent } from './seller-overview/seller-overview.component';
import { ListFileUploadComponent } from './list-file-upload/list-file-upload.component';
import { ListBannerComponent } from './list-banner/list-banner.component';
import { ListConfigSystemComponent } from './list-system-config/list-system-config.component';
import { CategoryComponent } from './category/category.component';

const routes: Routes = [
  {
    path: "list-news",
    component: ListNewsComponent
  },
  {
    path: "list-file-upload",
    component: ListFileUploadComponent
  },
  {
    path: "list-banners",
    component: ListBannerComponent
  },
  {
    path: "list-config-system",
    component: ListConfigSystemComponent
  },
  {
    path: "product-details",
    component: ProductDetailsComponent
  },
  {
    path: "add-product",
    component: AddProductComponent
  },
  {
    path: "orders",
    component: OrdersComponent
  },
  {
    path: "order-overview",
    component: OrderOverviewComponent
  },
  {
    path: "customers",
    component: CustomersComponent
  },
  {
    path: "cart",
    component: CartComponent
  },
  {
    path: "checkout",
    component: CheckoutComponent
  },
  {
    path: "sellers",
    component: SellersComponent
  },
  {
    path: "seller-overview",
    component: SellerOverviewComponent
  },
  {
    path: "category",
    component: CategoryComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagerSystemRoutingModule { }
