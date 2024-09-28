import { DecimalPipe } from '@angular/common';
import { Component, ViewChild, QueryList, ViewChildren } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Store } from '@ngrx/store';
import { selectData, selectlistData } from 'src/app/store/Invoices/invoices.selector';
import { deleteinvoice, fetchInvoiceData, fetchInvoicelistData } from 'src/app/store/Invoices/invoices.action';
import { PurchaseOrder } from 'src/app/models/models/purchase';
import { PurchaseServices } from 'src/app/core/services/purchase.service';
import { PageResult } from 'src/app/models/models/repository_base';
import { PurchaseOrderCode } from 'src/app/models/enum/etype_project.enum';

@Component({
  selector: 'app-list-purchase-order',
  templateUrl: './list-purchase-order.component.html',
  styleUrls: ['./list-purchase-order.component.scss'],
  providers: [DecimalPipe]
})

// List Component
export class ListPurchaseOrderComponent {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  invoiceslist: PageResult<PurchaseOrder[]>;
  invoices: PurchaseOrder[] = [];
  deleteID: any;
  masterSelected!: boolean;
  invoiceCard: any;
  term: any
  @ViewChild('deleteRecordModal', { static: false }) deleteRecordModal?: ModalDirective;

  constructor(
    public store: Store,
    private readonly purchaseServices: PurchaseServices,) {
  }

  ngOnInit(): void {
    /**
     * BreadCrumb
     */
    this.breadCrumbItems = [
      { label: 'List of Purchase', active: true }
    ];

    this.store.dispatch(fetchInvoiceData());
    this.store.select(selectData).subscribe((data) => {
      this.invoiceCard = data;
      console.table(this.invoiceCard);
    });
    this.getListPurchaseOrderOfCourse(1, 10);
  }

  // Sort Data
  direction: any = 'asc';
  onSort(column: any) {
    if (this.direction == 'asc') {
      this.direction = 'desc';
    } else {
      this.direction = 'asc';
    }
    const sortedArray = [...this.invoices]; // Create a new array
    sortedArray.sort((a, b) => {
      const res = this.compare(a[column], b[column]);
      return this.direction === 'asc' ? res : -res;
    });
    this.invoices = sortedArray;
  }
  compare(v1: string | number, v2: string | number) {
    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
  }

  // filterdata
  filterdata() {
    if (this.term) {
      this.invoices = this.invoiceslist.results.filter((el: any) => el.customer.toLowerCase().includes(this.term.toLowerCase()))
    } else {
      this.invoices = this.invoiceslist.results;
    }
    // noResultElement
    this.updateNoResultDisplay();
  }

  // no result 
  updateNoResultDisplay() {
    const noResultElement = document.querySelector('.noresult') as HTMLElement;

    if (this.term && this.invoices.length === 0) {
      noResultElement.style.display = 'block';
    } else {
      noResultElement.style.display = 'none';
    }
  }


  checkedValGet: any[] = [];
  // The master checkbox will check/ uncheck all items
  checkUncheckAll(ev: any) {
    var checkedVal: any[] = [];
    var result;
    for (var i = 0; i < this.invoices.length; i++) {
      if (this.invoices[i].isCheck == true) {
        result = this.invoices[i].id;
        checkedVal.push(result);
      }
    }

    this.checkedValGet = checkedVal;
    checkedVal.length > 0 ? document.getElementById("remove-actions")?.classList.remove('d-none') : document.getElementById("remove-actions")?.classList.add('d-none');
  }
  // Select Checkbox value Get
  onCheckboxChange(e: any) {
    var checkedVal: any[] = [];
    var result
    for (var i = 0; i < this.invoices.length; i++) {
      if (this.invoices[i].isCheck == true) {
        result = this.invoices[i].id;
        checkedVal.push(result);
      }
    }
    this.checkedValGet = checkedVal
    checkedVal.length > 0 ? document.getElementById("remove-actions")?.classList.remove('d-none') : document.getElementById("remove-actions")?.classList.add('d-none');
  }

  // Delete Product
  removeItem(id: any) {
    this.deleteID = id
    this.deleteRecordModal?.show()
  }

  deleteData(id: any) {
    this.deleteRecordModal?.hide();
    if (id) {
      this.store.dispatch(deleteinvoice({ id: this.deleteID.toString() }));
    }
    this.store.dispatch(deleteinvoice({ id: this.checkedValGet.toString() }));
    this.deleteRecordModal?.hide();
    this.masterSelected = false
  }

  // Page Changed
  pageChanged(event: any): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.invoices = this.invoiceslist.results
      .slice(startItem, endItem);
  }

  getListPurchaseOrderOfCourse(pageIndex: number, pageSize: number) {
    this.purchaseServices.getListPurchaseOrder(pageIndex, pageSize).subscribe((res) => {
      if (res.retCode === 0 || res.systemMessage === '') {
        this.invoiceslist = res.data;
        this.invoices = this.invoiceslist.results;
        document.getElementById('elmLoader')?.classList.add('d-none');
      } else {
        document.getElementById('elmLoader')?.classList.add('d-none')
      }
    });
  }

  confirmOrder(model: PurchaseOrder) {
    model.purcharseStatus == PurchaseOrderCode.Payment;
    this.purchaseServices.updateStatusPurchase(model).subscribe((res) => {
      if (res.retCode === 0 || res.systemMessage === '') {
        this.getListPurchaseOrderOfCourse(1, 10);
      } else {
        document.getElementById('elmLoader')?.classList.add('d-none')
      }
    })
  }


}
