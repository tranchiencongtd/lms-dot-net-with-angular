import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./dashboards/dashboards.module').then((m) => m.DashboardsModule),
  },
  {
    path: 'manager-system',
    loadChildren: () =>
      import('./manager-system/manager-system.module').then((m) => m.ManagerSystemModule),
  },
  {
    path: 'learning',
    loadChildren: () =>
      import('./learning/learning.module').then((m) => m.LearningModule),
  },
  {
    path: 'purchase-order',
    loadChildren: () =>
      import('./purchase-order/purchase-order.module').then((m) => m.PurchaseOrderModule),
  },
  {
    path: 'advance-ui',
    loadChildren: () =>
      import('./advanceui/advanceui.module').then((m) => m.AdvanceuiModule),
  },
  {
    path: 'subscriptions',
    loadChildren: () =>
      import('./subscriptions/subscriptions.module').then(
        (m) => m.SubscriptionsModule
      ),
  },
  {
    path: 'user-manager',
    loadChildren: () =>
      import('./user-manager/user-manager.module').then(
        (m) => m.UserManagerModule
      ),
  },
  {
    path: 'charts',
    loadChildren: () =>
      import('./charts/charts.module').then((m) => m.ChartsModule),
  },
  {
    path: 'custom-ui',
    loadChildren: () =>
      import('./custom-ui/custom-ui.module').then((m) => m.CustomUiModule),
  },
  {
    path: 'pages',
    loadChildren: () =>
      import('./extrapages/extrapages.module').then((m) => m.ExtrapagesModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
