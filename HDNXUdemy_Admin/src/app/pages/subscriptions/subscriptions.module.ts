import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Page Route
import { SubscriptionsRoutingModule } from './subscriptions-routing.module';
import { SharedModule } from '../../shared/shared.module';

// Count To
import { CountUpModule } from 'ngx-countup';

// Flat Picker
import { FlatpickrModule } from 'angularx-flatpickr';

// Simplebar
import { SimplebarAngularModule } from 'simplebar-angular';

// bootstrap component
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

// Component
import { ListPromotionsComponent } from './list-subscriptions/list-promotion.component';
import { OverviewComponent } from './overview/overview.component';
import { ListCouponsComponent } from './list-coupons/list-coupons.component';

@NgModule({
  declarations: [
    ListPromotionsComponent,
    OverviewComponent,
    ListCouponsComponent
  ],
  imports: [
    CommonModule,
    SubscriptionsRoutingModule,
    SharedModule,
    CountUpModule,
    PaginationModule,
    ModalModule,
    FormsModule,
    ReactiveFormsModule,
    SimplebarAngularModule,
    FlatpickrModule.forRoot(),
    TooltipModule
  ],
  providers:[DatePipe]
})
export class SubscriptionsModule { }
