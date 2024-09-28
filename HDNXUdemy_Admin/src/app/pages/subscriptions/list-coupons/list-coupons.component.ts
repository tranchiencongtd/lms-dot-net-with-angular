import { Component, ViewChild } from '@angular/core';

// Get Modal
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { assignesTickets } from 'src/app/core/data';
import { StripeServices } from 'src/app/core/services/stripe.service';
import { CouponModel } from 'src/app/models/models/coupon';
import { PageResult } from 'src/app/models/models/repository_base';
import { CouponPromotionModel } from 'src/app/models/request_model/coupon-promotion';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { Messenger } from 'src/app/models/contants/ennum_router';

@Component({
  selector: 'app-list-coupons',
  templateUrl: './list-coupons.component.html',
  styleUrls: ['./list-coupons.component.scss'],
  providers: [DecimalPipe]
})

// List component
export class ListCouponsComponent {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  deleteId: string;
  endItem: any
  listCouponForm!: UntypedFormGroup;
  masterSelected!: boolean;
  supportList: any;
  coupons: PageResult<CouponModel[]>;
  // assigndata: any
  assignList: any;
  term: any
  @ViewChild('addCoupons', { static: false }) addCoupon?: ModalDirective;
  @ViewChild('deleteRecordModal', { static: false }) deleteRecordModal?: ModalDirective;
  assignto: CouponModel[] = [];
  editData: CouponModel;
  isLoading = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    public datepipe: DatePipe,
    private readonly stripeServices: StripeServices,
    private readonly messengerServices: MessengerServices) {
  }

  ngOnInit(): void {
    /**
     * BreadCrumb
     */
    this.breadCrumbItems = [
      { label: 'Home', active: true },
      { label: 'List Coupon', active: true }
    ];

    /**
     * Form Validation
     */
    this.listCouponForm = this.formBuilder.group({
      id: [''],
      percentOff: ['', [Validators.required]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      status: ['', [Validators.required]],
      nameOfCoupon: ['', [Validators.required]]
    });

    this.assignList = assignesTickets;
    this.loadDataOfCoupon(1, 10);
  }

  // Edit Data
  editList(id: any) {
    this.addCoupon?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Edit Value Of Coupon'
    var modal = document.getElementById('add-btn') as HTMLAreaElement
    modal.innerHTML = 'Update';
    this.editData = this.coupons.results[id];
    this.listCouponForm.patchValue(this.coupons.results[id]);

    const startDate = this.datepipe.transform(this.coupons.results[id].startDate, "MM/dd/yyyy hh:mm") || ''
    const endDate = this.datepipe.transform(this.coupons.results[id].endDate, "MM/dd/yyyy hh:mm") || ''
    this.listCouponForm.patchValue({ startDate: startDate, endDate: endDate });
    console.log(this.listCouponForm.value);
  }

  addNewCoupon() {
    this.addCoupon?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Add Value Of Coupon'
    var modal = document.getElementById('add-btn') as HTMLAreaElement
    modal.innerHTML = 'Add Coupon';
    this.listCouponForm.reset();
  }

  // Add Assigne
  addAssign(id: any) {
    if (this.assignList[id].checked == '0') {
      this.assignList[id].checked = '1'
    } else {
      this.assignList[id].checked = '0'
    }

    this.assignto = [];
    this.assignList.forEach((element: any) => {
      if (element.checked == '1') {
        this.assignto.push(element)
      }
    });
  }

  // add Product
  saveForCouponPromotion() {
    if (this.listCouponForm.valid) {
      if (this.listCouponForm.get('id')?.value) {
        const updatedData = this.listCouponForm.value as CouponPromotionModel;
        this.stripeServices.updateCouponPromotionCode(updatedData).subscribe((res) => {
          if (res.systemMessage === '' && res.retCode === 0) {
            this.messengerServices.successes(Messenger.updateSuccessFull);
            this.loadDataOfCoupon(1, 10);
          } else {
            this.messengerServices.errorWithIssue();
            this.loadDataOfCoupon(1, 10);
          }
        })
      }
      else {
        this.stripeServices.createCouponPromotionCode(this.listCouponForm.value as CouponPromotionModel).subscribe((res) => {
          if (res.systemMessage === '' && res.retCode === 0) {
            this.messengerServices.successes(Messenger.createDataSuccessFull);
            this.loadDataOfCoupon(1, 10);
          } else {
            this.messengerServices.errorWithIssue();
            this.loadDataOfCoupon(1, 10);
          }
        })
      }
      this.listCouponForm.reset();
      this.addCoupon?.hide()
    }
  }

  checkedValGet: any[] = [];
  // Delete Product
  removeItem(id: string) {
    this.deleteId = id;
    this.deleteRecordModal?.show()
  }

  deleteData(stripeCouponId: string) {
    this.deleteRecordModal?.hide();
    if (stripeCouponId) {
      this.stripeServices.deleteCouponForPromotion(stripeCouponId).subscribe((res) => {
        if (res.systemMessage === '' && res.retCode === 0) {
          this.messengerServices.successes(Messenger.updateSuccessFull);
          this.deleteRecordModal?.hide();
          this.masterSelected = false;
          this.loadDataOfCoupon(1, 10);
        } else {
          this.messengerServices.errorWithIssue();
          this.deleteRecordModal?.hide();
          this.masterSelected = false;
          this.loadDataOfCoupon(1, 10);
        }
      })
    }
  }
  // Sort Data
  direction: any = 'asc';
  onSort(column: any) {
    if (this.direction == 'asc') {
      this.direction = 'desc';
    } else {
      this.direction = 'asc';
    }
    const sortedArray = [...this.coupons.results]; // Create a new array
    sortedArray.sort((a, b) => {
      const res = this.compare(a[column], b[column]);
      return this.direction === 'asc' ? res : -res;
    });
    this.coupons.results = sortedArray;
  }
  compare(v1: string | number, v2: string | number) {
    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
  }


  // filterdata
  filterdata() {
    // if (this.term) {
    //   this.coupons.results = this.alltickets.filter((es: any) => es.ticketTitle.toLowerCase().includes(this.term.toLowerCase()))
    // } else {
    //   this.coupons.results = this.alltickets.slice(0, 10);
    // }
    // noResultElement
    this.updateNoResultDisplay();
  }

  // no result 
  updateNoResultDisplay() {
    const noResultElement = document.querySelector('.noresult') as HTMLElement;
    const paginationElement = document.getElementById('pagination-element') as HTMLElement;

    if (this.term && this.coupons.results.length === 0) {
      noResultElement.classList.remove('d-none')
      paginationElement.classList.add('d-none')

    } else {
      noResultElement.classList.add('d-none')
      paginationElement.classList.remove('d-none')
    }
  }

  // pagechanged
  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    this.endItem = event.page * event.itemsPerPage;
    this.loadDataOfCoupon(startItem, 10);
  }

  loadDataOfCoupon(pageIndex: number, pageSize: number) {
    this.isLoading = true;
    this.stripeServices.getListCouponAndPromotion(pageIndex, pageSize).subscribe((res) => {
      if (res.systemMessage === '' && res.retCode === 0) {
        this.isLoading = false;
        this.coupons = res.data;
      } else {
        this.isLoading = false;
      }
    });
  }

  activeTheCoupon(id: number) {
    this.stripeServices.activeCouponPromotion(id).subscribe((res) => {
      if (res.systemMessage === '' && res.retCode === 0) {
        this.isLoading = false;
        this.loadDataOfCoupon(1, 10);
      } else {
        this.isLoading = false;
      }
    });
  }
}


