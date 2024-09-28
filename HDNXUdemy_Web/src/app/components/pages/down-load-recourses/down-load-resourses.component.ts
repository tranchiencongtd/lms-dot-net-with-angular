import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-down-load-recourses',
    templateUrl: './down-load-recourses.component.html',
    styleUrls: ['./down-load-recourses.component.scss']
})
export class DownLoadRecoursesComponent implements OnInit {

    constructor() { }

    ngOnInit(): void {}

    // Tabs
    currentTab = 'tab1';
    switchTab(event: MouseEvent, tab: string) {
        event.preventDefault();
        this.currentTab = tab;
    }

}