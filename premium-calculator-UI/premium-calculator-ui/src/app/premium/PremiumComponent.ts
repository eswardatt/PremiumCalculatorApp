import { Component, OnInit, signal } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PremiumService } from '../services/PremiumService';
import { Occupation } from '../Models/PremiumRequest';

@Component({
  selector: 'app-premium',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './premium.component.html'
})
export class PremiumComponent implements OnInit {

  occupations: Occupation[] = [];
  premium = signal<number | null>(null);

  form = new FormGroup({
    name: new FormControl('', Validators.required),
    ageNextBirthday: new FormControl<number | null>(null, Validators.required),
    dob: new FormControl('', Validators.required),
    occupationId: new FormControl<number | null>(null, Validators.required),
    deathSumInsured: new FormControl<number | null>(null, Validators.required)
  });

  constructor(private service: PremiumService) { }

  ngOnInit(): void {
    this.service.getOccupations().subscribe(res => this.occupations = res);

    // Auto-trigger premium calculation when occupation changes
    this.form.get('occupationId')?.valueChanges.subscribe(() => {
      this.calculate();
    });
  }

  calculate() {
    if (this.form.invalid) {
      this.premium.set(null);
      return;
    }

    const payload = this.form.getRawValue() as any;

    this.service.calculatePremium(payload).subscribe(res => {
      this.premium.set(res.monthlyPremium);
    });
  }
}
