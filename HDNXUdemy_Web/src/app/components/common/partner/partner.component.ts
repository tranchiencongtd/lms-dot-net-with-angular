import { Component, Input, OnInit } from '@angular/core';
import { Partner } from 'src/app/models/models/partner';

@Component({
  selector: 'app-partner',
  templateUrl: './partner.component.html',
  styleUrls: ['./partner.component.scss']
})
export class PartnerComponent implements OnInit {

  constructor() { }

  @Input() listPartner?: Partner[];

  ngOnInit(): void {
  }

}
