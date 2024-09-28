import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// component
import { BreadcrumbsComponent } from './breadcrumbs/breadcrumbs.component';
import { HdnxLoadingComponent } from './hdnx-loading/hdnx-loading.component';

@NgModule({
  declarations: [
    BreadcrumbsComponent,
    HdnxLoadingComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [BreadcrumbsComponent, HdnxLoadingComponent]
})
export class SharedModule { }
