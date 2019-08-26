import { Component, OnInit, Renderer, OnDestroy } from '@angular/core';
import { NgbAccordionConfig } from '@ng-bootstrap/ng-bootstrap';
import * as Rellax from 'rellax';
import { ApiService } from 'app/api/api.service';

@Component({
    selector: 'app-components',
    templateUrl: './components.component.html',
    styles: [`
    ngb-progressbar {
        margin-top: 5rem;
    }
    `]
})

export class ComponentsComponent implements OnInit, OnDestroy {
    all_sql_results: {};
    all_xml_results: {};
    data : Date = new Date();

    state_icon_primary = true;

    constructor( private renderer : Renderer, config: NgbAccordionConfig, private apiService: ApiService) {
        config.closeOthers = true;
        config.type = 'info';
    }
    

    ngOnInit() {
        
        this.GetAllResults();
        
      var rellaxHeader = new Rellax('.rellax-header');

        var navbar = document.getElementsByTagName('nav')[0];
        navbar.classList.add('navbar-transparent');
        var body = document.getElementsByTagName('body')[0];
        body.classList.add('index-page');
    }
    ngOnDestroy(){
        var navbar = document.getElementsByTagName('nav')[0];
        navbar.classList.remove('navbar-transparent');
        var body = document.getElementsByTagName('body')[0];
        body.classList.remove('index-page');
    }

    async GetAllResults()
    {
        this.all_sql_results = await this.apiService.getAllResults();
        this.all_xml_results = await this.apiService.getAllXmlResults();
    }
}
