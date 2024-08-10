import { Component } from '@angular/core';
import { AdviseService } from '../../service/advise.service';
import { Router } from '@angular/router';
import { ListService } from '../list.service';
import { LoaderService } from '../../service/loader.service';

@Component({
  selector: 'app-list-container',
  templateUrl: './list-container.component.html',
  styleUrl: './list-container.component.scss',
})
export class ListContainerComponent {
  searchValue: string = '';

  constructor(
    public adviceService: AdviseService,
    private readonly router: Router,
    private listService: ListService,
    private loaderService: LoaderService
  ) {}

  search(searchValue: string) {
    this.searchValue = searchValue.trim();
    this.loaderService.show();
    if ((this.listService.gridState.filters = this.getFilters())) {
      this.listService.gridState.operators = 'or';
      this.listService.read();
      this.loaderService.hide();
    }
  }

  getFilters() {
    return {
      Name: [{ value: this.searchValue, matchMode: 'contains' }],
      SIN: [{ value: this.searchValue, matchMode: 'contains' }],
      Phone: [{ value: this.searchValue, matchMode: 'contains' }],
    };
  }
  clearSearch() {
    this.searchValue = '';
    this.search(this.searchValue);
  }
}
