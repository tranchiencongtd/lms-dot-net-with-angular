import { Course } from "../models/course";
import { CourseContent, CourseContentDetails } from "../models/course-content";

export class CourseContentWithDetails extends CourseContent {
    contentAndContentDetails: CourseContentDetails[];
}

export class GetCourseWithDetailsContent extends Course {
    listContentCourseDetails : CourseContentWithDetails[];
}


export class ContentAndDetails extends CourseContent {
    listContentCourseDetails: CourseContentDetails[];
}