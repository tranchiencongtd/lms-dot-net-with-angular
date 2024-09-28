import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-apply-instructor',
    templateUrl: './apply-instructor.component.html',
    styleUrls: ['./apply-instructor.component.scss']
})
export class ApplyInstructorComponent implements OnInit {

    constructor() { }

    ngOnInit(): void {}

    // Tabs
    currentTab = 'tab1';
    switchTab(event: MouseEvent, tab: string) {
        event.preventDefault();
        this.currentTab = tab;
    }

}