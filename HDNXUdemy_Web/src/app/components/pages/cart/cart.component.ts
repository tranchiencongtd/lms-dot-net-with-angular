import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationServices } from 'src/app/core/services/authentication.service';
import { CartServices } from 'src/app/core/services/cart.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { PurchaseServices } from 'src/app/core/services/purchase.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { GetCourseWithDetailsContent } from 'src/app/models/respone_model/course-content-with-detail';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  totalValue = 0;
  constructor(
    public readonly cartServices: CartServices,
    private readonly purchaseServices: PurchaseServices,
    private readonly router: Router,
    private readonly messengerServices: MessengerServices,
    private readonly authenticationServices : AuthenticationServices,
  ) { }

  ngOnInit(): void {
    this.totalValue = this.cartServices.courseItems.reduce((c, t1) => t1.totalOrder + c, 0);
  }

  removeItemOnCart(item: GetCourseWithDetailsContent) {
    this.cartServices.removeCourse(item);
    this.totalValue = this.cartServices.courseItems.reduce((c, t1) => t1.totalOrder + c, 0);
  }

  goToPaymentForCourse() {
    let getDataOfUser = LocalStorageConfig.GetUser();
    if (getDataOfUser === null) {
      this.messengerServices.warringBookMarkCourse('Vui lòng đăng nhập để thực hiện thanh toán!');
    } else {
      
      this.purchaseServices.genPurchaseOrder(getDataOfUser.userId).subscribe((res) => {
        if (res.retCode === 0 && res.systemMessage === '') {
          this.router.navigate([`/payment/${res.data}`]);
        }
      })
    }
  }

  get isMemberAccount(){
    return this.authenticationServices.isCheckMemberAccount();
}

}
