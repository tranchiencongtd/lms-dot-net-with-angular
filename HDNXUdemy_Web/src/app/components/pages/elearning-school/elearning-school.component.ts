import { Component, OnInit } from '@angular/core';
import { HomeServices } from 'src/app/core/services/home.service';
import { HomeDataModel } from 'src/app/models/respone_model/home-data';

@Component({
    selector: 'app-elearning-school',
    templateUrl: './elearning-school.component.html',
    styleUrls: ['./elearning-school.component.scss']
})
export class ElearningSchoolComponent implements OnInit {

    constructor(
        private readonly homeServices: HomeServices) { }

    ngOnInit(): void {
        this.loadDataOfElearning();
    }

    homeDataOfProject: HomeDataModel | null | undefined;
    isLoading: boolean = false;

    loadDataOfElearning() {
        this.isLoading = true;
        this.homeServices.getHomeData().subscribe((res) => {
            if (res.retCode === 0 && res.systemMessage === '') {
                this.homeDataOfProject = res.data;
                this.isLoading = false;
            } else {
                this.isLoading = false;
            }
        });
    }

}