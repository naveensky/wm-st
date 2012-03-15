using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using StudentTracker.Models;
//using StudentTracker.Site.ViewModels.Appointment;
using StudentTracker.Site.ViewModels.Appointment;
using StudentTracker.Site.ViewModels.Common;
using StudentTracker.Site.ViewModels.Student;
using StudentTracker.Site.ViewModels.Teacher;
using StudentTracker.Site.ViewModels.Time;
using StudentTracker.Site.ViewModels.User;
using Topic = StudentTracker.Models.Topic;

namespace StudentTracker.Mappings {
    public static class MapperExtensions {

        public static CourseViewModel MapToView(this Course course) {
            return Mapper.Map<Course, CourseViewModel>(course);
        }

        public static StudyCenterViewModel MapToView(this StudyCenter center) {
            return Mapper.Map<StudyCenter, StudyCenterViewModel>(center);
        }

        public static Student MapToDomain(this StudentRegisterViewModel studentViewModel) {
            return Mapper.Map<StudentRegisterViewModel, Student>(studentViewModel);
        }

        public static IEnumerable<StudentViewModel> MapToView(this IEnumerable<Models.Student> students) {
            return Mapper.Map<IEnumerable<Models.Student>, IEnumerable<StudentViewModel>>(students);
        }

        public static StudentViewModel MapToView(this Models.Student students) {
            return Mapper.Map<Models.Student, StudentViewModel>(students);
        }

        public static Student MapToDomain(this StudentViewModel studentViewModel) {
            return Mapper.Map<StudentViewModel, Student>(studentViewModel);
        }

        public static IEnumerable<TeacherViewModel> MapToView(this IEnumerable<Models.Teacher> teachers) {
            return Mapper.Map<IEnumerable<Models.Teacher>, IEnumerable<TeacherViewModel>>(teachers);
        }

        public static NewAppointmentViewModel MapToView(this Models.Appointment appointment) {
            return Mapper.Map<Models.Appointment, NewAppointmentViewModel>(appointment);
        }

        public static TeacherViewModel MapToView(this Models.Teacher teacher) {
            return Mapper.Map<Models.Teacher, TeacherViewModel>(teacher);
        }

        public static Appointment MapToDomain(this NewAppointmentViewModel newAppointmentViewModel) {
            return Mapper.Map<NewAppointmentViewModel, Appointment>(newAppointmentViewModel);
        }

        public static Teacher MapToDomain(this TeacherViewModel teacherViewModel) {
            return Mapper.Map<TeacherViewModel, Teacher>(teacherViewModel);
        }

        public static IEnumerable<StudyCenterViewModel> MapToView(this IEnumerable<StudyCenter> studyCenters) {
            return Mapper.Map<IEnumerable<StudyCenter>, IEnumerable<StudyCenterViewModel>>(studyCenters);
        }

        public static User MapToDomain(this UserViewModel userViewModel) {
            return Mapper.Map<UserViewModel, User>(userViewModel);
        }

        public static StudyCenter MapToDomain(this StudyCenterViewModel studyCenterViewModel) {
            return Mapper.Map<StudyCenterViewModel, StudyCenter>(studyCenterViewModel);
        }

        public static Time MapToDomain(this TimeViewModel timeViewModel) {
            return Mapper.Map<TimeViewModel,Time>(timeViewModel);
        }

        public static Site.ViewModels.Student.AppointmentViewModel MapToView(this AppointMentList appointMentLists) {
            return Mapper.Map<AppointMentList,Site.ViewModels.Student.AppointmentViewModel>(appointMentLists);
        }
        public static void ConfigureMappings() {
            Mapper.CreateMap<Course, StudentTracker.Site.ViewModels.Common.CourseViewModel>()
                .ForMember(d => d.Topics, s => s.MapFrom(src => src.Topics));
            Mapper.CreateMap<Topic, TopicViewModel>();
            Mapper.CreateMap<StudyCenter, StudyCenterViewModel>();
            Mapper.CreateMap<StudyCenterViewModel, StudyCenter>();
            Mapper.CreateMap<StudentRegisterViewModel, Student>().ForMember(d => d.Course, s => s.Ignore()).ForMember(d => d.StudyCenter, s => s.Ignore()).ForMember(d=>d.Appointments,s=>s.Ignore()).ForMember(x=>x.Appointments,s=>s.Ignore()).ForMember(x=>x.Appointments,s=>s.Ignore());
            Mapper.CreateMap<IEnumerable<Models.Student>, IEnumerable<StudentViewModel>>();
            Mapper.CreateMap<Student, StudentViewModel>();
            Mapper.CreateMap<StudentViewModel, Student>().ForMember(d => d.Course, s => s.Ignore()).ForMember(d => d.StudyCenter, s => s.Ignore()).ForMember(d=>d.Appointments,s=>s.Ignore()); ;
            Mapper.CreateMap<IEnumerable<Models.Teacher>, IEnumerable<TeacherViewModel>>();
            Mapper.CreateMap<Models.Appointment,NewAppointmentViewModel>().ForMember(d=>d.TopicId,s=>s.Ignore())
             .ForMember(d=>d.Topic,s=>s.Ignore())
             .ForMember(d=>d.EndTime,s=>s.Ignore()).ForMember(d=>d.StartTime,s=>s.Ignore()).ForMember(d=>d.SelectedTeacherId,s=>s.Ignore())
             .ForMember(d=>d.Teachers,s=>s.Ignore()).ForMember(d=>d.Student,s=>s.Ignore());
            Mapper.CreateMap<Models.Teacher, TeacherViewModel>();
            Mapper.CreateMap<NewAppointmentViewModel, Appointment>().ForMember(d => d.Topic, s => s.Ignore()).ForMember(d => d.Teacher, s => s.Ignore()).ForMember(d => d.Duration, s => s.Ignore()).ForMember(d=>d.Students,s=>s.Ignore()).ForMember(d=>d.StartTime,s=>s.Ignore()).ForMember(d=>d.EndTime,d=>d.Ignore());
            Mapper.CreateMap<TeacherViewModel, Teacher>().ForMember(d=>d.Appointments,s=>s.Ignore());
            Mapper.CreateMap<IEnumerable<StudyCenter>, IEnumerable<StudyCenterViewModel>>();
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<TimeViewModel, Time>();
            Mapper.CreateMap<AppointMentList,Site.ViewModels.Student.AppointmentViewModel>();
            Mapper.AssertConfigurationIsValid();
        }

    }
}
