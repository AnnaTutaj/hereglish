import { Component, OnInit } from '@angular/core';

@Component({
    templateUrl: './admin.component.html',
})

export class AdminComponent implements OnInit {
    //Line
    public lineChartData: Array<any> = [
        { data: [65, 59, 80, 81, 56, 55, 40], label: 'Learned' },
        { data: [28, 48, 40, 19, 86, 27, 90], label: 'Not learned' },
    ];
    public lineChartLabels: Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
    public lineChartType: string = 'line';
    public pieChartType: string = 'pie';

    // Pie
    public pieChartLabels: string[] = ['Nouns', 'Verbs', 'Adjectives'];
    public pieChartData: number[] = [300, 500, 100];

    public randomizeType(): void {
        this.lineChartType = this.lineChartType === 'line' ? 'bar' : 'line';
        this.pieChartType = this.pieChartType === 'doughnut' ? 'pie' : 'doughnut';
    }
    
    //Bar
    public barChartLabels: string[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    public barChartType: string = 'bar';
    public barChartLegend: boolean = true;

    public barChartData: any[] = [
        { data: [65, 59, 80, 81, 56, 55, 40], label: 'New Words' },
        { data: [28, 48, 40, 19, 86, 27, 90], label: 'Marked as learned' }
    ];

    public randomize(): void {
        // Only Change 3 values
        let data = [
            Math.round(Math.random() * 100),
            59,
            80,
            (Math.random() * 100),
            56,
            (Math.random() * 100),
            40];
        let clone = JSON.parse(JSON.stringify(this.barChartData));
        clone[0].data = data;
        this.barChartData = clone;
        /**
         * (My guess), for Angular to recognize the change in the dataset
         * it has to change the dataset variable directly,
         * so one way around it, is to clone the data, change it and then
         * assign it;
         */
    }

    //events
    public chartClicked(e: any): void {
        console.log(e);
    }

    public chartHovered(e: any): void {
        console.log(e);
    }

    constructor() { }

    ngOnInit() { }
}