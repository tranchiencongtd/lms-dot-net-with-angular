import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartServices } from 'src/app/core/services/cart.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { PurchaseServices } from 'src/app/core/services/purchase.service';
import { StripeServices } from 'src/app/core/services/stripe.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { ConstantValue } from 'src/app/models/contants/ennum_router';
import { InformationManualBankingModel } from 'src/app/models/models/infomation-banking';
import { PurchaseOrder } from 'src/app/models/models/purchase';
import { PurchaseDetailsModel } from 'src/app/models/models/purchase-details';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  constructor(
    private readonly router: ActivatedRoute,
    private readonly routers: Router,
    private readonly purchaseServices: PurchaseServices,
    private readonly cartServices: CartServices,
    private readonly messengerServices: MessengerServices,
    private readonly stripeServices: StripeServices,
  ) { }
  purchaseCode: string = '';
  infomationBanking: InformationManualBankingModel;
  totalValue = 0;
  stripePayment = ConstantValue.paymentStripe;
  tranferPayment = ConstantValue.paymentTranfer;
  ngOnInit(): void {
    this.purchaseCode = this.router.snapshot.paramMap.get('purchaseCode');
    this.getInfomationBanking();
    this.totalValue = this.cartServices.courseItems.reduce((c, t1) => t1.totalOrder + c, 0);
  }

  copyMessenger(text: string) {
    navigator.clipboard.writeText(text).then(
      () => {
        console.log('Text copied to clipboard!');
      }
    );

    navigator.clipboard.readText().then(
      (text) => {
        console.log(text);
      }
    )
  }

  getInfomationBanking() {
    this.purchaseServices.getInformationOfBanking().subscribe((res) => {
      if (res.retCode === 0 && res.systemMessage === '') {
        this.infomationBanking = res.data;
      }
    })
  }

  paymentPurchaseCourse(typePayment: string) {
    let getDataOfUser = LocalStorageConfig.GetUser();
    let dataOfCart = this.cartServices.courseItems;
    let dataDetailsOfCourseDetail: PurchaseDetailsModel[] = [];
    dataOfCart.forEach((item) => {
      let dataDetailItem: PurchaseDetailsModel = {
        idCourse: item.id,
        priceOfCourse: item.priceOfCourse,
        priceOfDiscount: item.priceOfCourse,
        idPurchaseOrder: 0,

      }
      dataDetailsOfCourseDetail.push(dataDetailItem);
    })

    let dataInsert: PurchaseOrder = {
      idStudent: getDataOfUser.userId,
      contentTranferBanking: this.purchaseCode,
      totalPrice: this.totalValue,
      discountAmount: 0,
      purcharseCode: this.purchaseCode,
      listPurchaseCourseDetails: dataDetailsOfCourseDetail,
      infoBanking: this.infomationBanking
    };

    switch (typePayment) {
      case this.stripePayment:
        this.stripeServices.goToCheckOutForStripe(dataInsert);
        break;
      case this.tranferPayment:
        this.purchaseServices.createRequestPurchase(dataInsert).subscribe((res) => {
          if (res.retCode === 0 && res.systemMessage === '') {
            this.cartServices.removeAllDataOfCart();
            this.messengerServices.confirmCreateOrder('Đơn hàng đang trong quá trình thanh toán, nếu thành công sẽ thông báo đến bạn!');
            this.routers.navigate(['/']);
          } else {
            this.messengerServices.errorWithIssue();
          }
        });
        break;
    }
  }
}
