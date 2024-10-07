using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversidad.Models;

public partial class UniversidadContext : DbContext
{
    public UniversidadContext()
    {
    }

    public UniversidadContext(DbContextOptions<UniversidadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnosPorCurso> AlumnosPorCursos { get; set; }

    public virtual DbSet<CarrerasUniversidad> CarrerasUniversidads { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<DocentesPorCurso> DocentesPorCursos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Universidad;Port=5432;User Id=postgres;Password=1201;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.ToTable("alumnos");

            entity.HasIndex(e => e.IdRol, "IX_alumnos_IdRol");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaAlta).HasDefaultValueSql("'-infinity'::timestamp with time zone");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Alumnos).HasForeignKey(d => d.IdRol);
        });

        modelBuilder.Entity<AlumnosPorCurso>(entity =>
        {
            entity.ToTable("alumnos_por_curso");

            entity.HasIndex(e => e.IdAlumno, "IX_alumnos_por_curso_IdAlumno");

            entity.HasIndex(e => e.IdCurso, "IX_alumnos_por_curso_IdCurso");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.AlumnosPorCursos).HasForeignKey(d => d.IdAlumno);

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.AlumnosPorCursos).HasForeignKey(d => d.IdCurso);
        });

        modelBuilder.Entity<CarrerasUniversidad>(entity =>
        {
            entity.ToTable("carreras_universidad");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.ToTable("cursos");

            entity.HasIndex(e => e.IdCarrera, "IX_cursos_IdCarrera");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Cursos).HasForeignKey(d => d.IdCarrera);
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.ToTable("docentes");

            entity.HasIndex(e => e.IdRol, "IX_docentes_IdRol");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaAlta).HasDefaultValueSql("'-infinity'::timestamp with time zone");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Docentes).HasForeignKey(d => d.IdRol);
        });

        modelBuilder.Entity<DocentesPorCurso>(entity =>
        {
            entity.ToTable("docentes_por_cursos");

            entity.HasIndex(e => e.IdCurso, "IX_docentes_por_cursos_IdCurso");

            entity.HasIndex(e => e.IdDocente, "IX_docentes_por_cursos_IdDocente");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.DocentesPorCursos).HasForeignKey(d => d.IdCurso);

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.DocentesPorCursos).HasForeignKey(d => d.IdDocente);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.HasIndex(e => e.IdRol, "IX_usuarios_IdRol");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaAlta).HasDefaultValueSql("'-infinity'::timestamp with time zone");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios).HasForeignKey(d => d.IdRol);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
