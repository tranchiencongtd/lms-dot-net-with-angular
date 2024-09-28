import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-elearning-banner',
    templateUrl: './elearning-banner.component.html',
    styleUrls: ['./elearning-banner.component.scss']
})
export class ElearningBannerComponent implements OnInit {

    constructor() { }

    ngOnInit(): void {
    }

    bgImage = [
        {
            img: 'assets/img/gray-bg.jpg'
        }
    ]

}