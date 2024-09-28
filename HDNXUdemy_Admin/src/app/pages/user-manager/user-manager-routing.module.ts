import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { ListUserManagerComponent } from './list-user-manager/list-user-manager.component';
import { AddEditAccountUserComponent } from './add-edit-account-user/add-edit-account-user.component';

const routes: Routes = [
  {
    path: 'add-edit-user-account/:id',
    component: AddEditAccountUserComponent,
  },
  {
    path: 'list-user-manager',
    component: ListUserManagerComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagerRoutingModule { }
