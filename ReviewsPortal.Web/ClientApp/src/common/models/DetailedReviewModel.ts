export interface DetailedReviewModel {
  title: string;
  artName: string;
  category: string;
  description: string;
  tags: string[];
  imageUrl: string;
  authorName: string;
  userRating: number;
  averageRating: number;
  likesCount: number;
  isLike: boolean;
  authorGrade: number;
  creationDate: Date;
}
