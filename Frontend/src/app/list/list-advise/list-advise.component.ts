import { Component, OnInit, ViewChild } from '@angular/core';
import { LoaderService } from '../../service/loader.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NotifyService } from '../../service/notify.service';
import { SettingMessage } from '../../core/model/toster.model';
import { AdviseService } from '../../service/advise.service';
import { ListService } from '../list.service';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-list-advise',
  templateUrl: './list-advise.component.html',
  styleUrl: './list-advise.component.scss',
  providers: [ConfirmationService, MessageService],
})
export class ListAdviseComponent implements OnInit {
  @ViewChild('gridtable') gridTable!: Table;
  public userForm: FormGroup;
  public advisors: any[] = [];
  public totalRecords: number = 0;
  public rows: number = 10;
  public currentPage: number = 1;
  public pageLinks: number = 5;
  public editingId?: number;
  public searchValue: string = '';

  constructor(
    public adviceService: AdviseService,
    private confirmationService: ConfirmationService,
    public listService: ListService,
    private loaderService: LoaderService,
    private readonly notifyService: NotifyService
  ) {
    this.userForm = new FormGroup({
      Name: new FormControl('', [
        Validators.required,
        Validators.maxLength(255),
      ]),
      SIN: new FormControl('', [Validators.required, Validators.maxLength(12)]),
      Address: new FormControl('', [Validators.maxLength(255)]),
      Phone: new FormControl('', [Validators.pattern(/^\d{4}-\d{4}$/)]),
    });
  }

  ngOnInit() {
    this.loaderService.show();
    this.listService.read();
    this.loaderService.hide();
  }

  public search(searchValue: string) {
    this.searchValue = searchValue.trim();
    if (searchValue.length > 0) {
      this.loaderService.show();
      this.listService.defaultFilter = this.getFilters();
      this.listService.gridState.operators = 'or';
      this.loaderService.hide();
    } else {
      this.listService.defaultFilter = null;
    }
    this.listService.onFilterChange({
      first: this.gridTable._first,
      rows: this.gridTable._rows,
      sortOrder: this.gridTable.sortOrder,
      filters: this.gridTable.filters,
      globalFilter: null,
    });
  }

  public getFilters() {
    if (this.searchValue.length > 0) {
      return {
        Name: [
          { value: this.searchValue, matchMode: 'contains', operator: 'or' },
        ],
        SIN: [
          { value: this.searchValue, matchMode: 'contains', operator: 'or' },
        ],
        Phone: [
          { value: this.searchValue, matchMode: 'contains', operator: 'or' },
        ],
        Address: [
          { value: this.searchValue, matchMode: 'contains', operator: 'or' },
        ],
      };
    }
    return null;
  }

  public clearSearch() {
    this.searchValue = '';
    this.search(this.searchValue);
  }

  public healthPercentage(healthStatus: string): number {
    return parseInt(healthStatus.replace('%', ''), 10);
  }

  public confirmDelete(AdvisorId: number) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this Advisor?',
      header: 'Confirmation',
      acceptIcon: 'mr-2',
      rejectIcon: 'mr-2',
      rejectButtonStyleClass: 'p-button-sm m-2 ',
      acceptButtonStyleClass: 'p-button-outlined p-button-sm m-2',
      accept: () => {
        this.deleteadvise(AdvisorId);
      },
      reject: () => {
        console.log('Delete cancelled');
      },
    });
  }

  public deleteadvise(id: number) {
    this.loaderService.show();
    this.adviceService.delete(id).subscribe({
      next: (res: any) => {
        this.notifyService.showSuccess(SettingMessage.deleteSuccess);
        this.listService.read();
        this.loaderService.hide();
      },
      error: (error: any) => {
        this.notifyService.showSuccess(SettingMessage.deleteError);
        console.error('Error deleting Advisor', error);
      },
    });
  }
}
