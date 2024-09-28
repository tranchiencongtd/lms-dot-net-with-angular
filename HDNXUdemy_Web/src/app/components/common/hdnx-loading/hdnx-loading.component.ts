import { ChangeDetectorRef, Component, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-hdnx-loading',
  templateUrl: './hdnx-loading.component.html',
  styleUrls: ['./hdnx-loading.component.scss']
})
export class HdnxLoadingComponent implements OnInit {
  @Input('show') showIf: boolean;
  diameter: number = 20;
  constructor() { }

  ngOnInit() {}

}
