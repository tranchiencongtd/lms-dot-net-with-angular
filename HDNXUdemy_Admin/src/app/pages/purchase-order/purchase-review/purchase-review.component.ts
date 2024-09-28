import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as L from "leaflet";
import "leaflet-routing-machine";
import { PurchaseServices } from 'src/app/core/services/purchase.service';
import { PurchaseOrder } from 'src/app/models/models/purchase';

@Component({
  selector: 'app-purchase-review',
  templateUrl: './purchase-review.component.html',
  styleUrls: ['./purchase-review.component.scss']
})

// order overview component
export class PurchaseOrderReviewComponent {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  purchaseOrderReview: PurchaseOrder;
  idPurchaseOrder: number;
  ngOnInit(): void {
    /**
     * BreadCrumb
     */
    this.breadCrumbItems = [
      { label: 'List of Purchase', active: true },
      { label: 'Detail Purchase Overview', active: true }
    ];

    this.idPurchaseOrder = Number(this.routers.snapshot.paramMap.get('id'));
    this.getPurchaseOrderReview(this.idPurchaseOrder);


    /**
   * Basic Maps
   */
    setTimeout(() => {
      const map = L.map('map'); // Set the initial map view
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
      }).addTo(map);
      L.Routing.control({
        waypoints: [
          L.latLng(57.74, 11.94),
          L.latLng(57.6792, 11.949)
        ],
        show: false,
        routeWhileDragging: true
      }).addTo(map);
    }, 500);
  }

  constructor(
    private readonly purchaseServices: PurchaseServices,
    private routers: ActivatedRoute,
  ) { }

  getPurchaseOrderReview(idPurchase: number) {
    this.purchaseServices.getPurchaseCourseDetail(idPurchase).subscribe((res) => {
      if (res.retCode === 0 || res.systemMessage === '') {
        this.purchaseOrderReview = res.data;
        document.getElementById('elmLoader')?.classList.add('d-none');
      } else {
        document.getElementById('elmLoader')?.classList.add('d-none')
      }
    })
  }
}

