import { Directive, Input, OnDestroy, OnInit } from '@angular/core';
import { Table } from 'primeng/table';
import { Subject, skip, take, takeUntil } from 'rxjs';
import { GridService } from '../services/grid.service';

@Directive({
  selector: '[appGridColumnFilter]',
})
export class GridColumnFilterDirective implements OnInit, OnDestroy {
  @Input() gridService!: GridService;
  private destroy$ = new Subject();

  constructor(private table: Table) {
    this.table.onLazyLoad.pipe(takeUntil(this.destroy$), skip(1)).subscribe({
      next: (res: any) => {
        this.gridService.onFilterChange(res);
      },
    });
  }

  ngOnInit(): void {
    this.gridService.data$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (res: any) => {
        this.table.value = res;
      },
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
