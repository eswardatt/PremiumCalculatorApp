export interface PremiumRequest {
  name: string;
  ageNextBirthday: number;
  dob: string; // MM/YYYY
  occupationId: number;
  deathSumInsured: number;
}

export interface PremiumResponse {
  monthlyPremium: number;
}

export interface Occupation {
  id: number;
  name: string;
  rating: string;
}
