﻿using Marqa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marqa.DataAccess.EntityConfigurations;

public class StudentPointHistoryConfiguration : IEntityTypeConfiguration<StudentPointHistory>
{
    public void Configure(EntityTypeBuilder<StudentPointHistory> builder)
    {
        builder.ToTable("StudentPointHistories");

        builder.HasOne(sph => sph.Student)
                .WithMany(s => s.StudentPointHistories)
                .HasForeignKey(sph  => sph.StudentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
    }
}

