using System;
using System.Reflection;
using GuerillaTactics.Common.Utility;   
using GuerillaTactics.Testing;
using NUnit.Framework;
using Rhino.Mocks;
using UnitTests;

namespace specs_for_ObjectFieldMapper
{
    public abstract class ObjectFieldMapperBaseContext : Specification<ObjectFieldMapper>
    {
        protected Source source;
        protected Target target;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            source = CreateSource();
            source.OtherSources.Add(CreateSource());
            source.OtherSources.Add(CreateSource());
            target = new Target { FieldWhichIsNotOnTheSource = 10 };
        }

        private Source CreateSource()
        {
            return new Source { One = "hello", Two = 5, Three = 5.ToString(), Four = 5, Five = 5, Six = 6};
        }
    }

    [TestFixture]
    public class in_general : ObjectFieldMapperBaseContext
    {
        protected override ObjectFieldMapper CreateSubject()
        {
            return new ObjectFieldMapper();
        }

        protected override void When()
        {
            Subject.Map(source, target);
        }

        [Test]
        public void simple_fields_of_the_same_type_can_be_mapped()
        {
            target.One.should_be_equal_to(source.One);
        }

        [Test]
        public void source_fields_which_are_not_strings_can_be_mapped_to_string_fields()
        {
            target.Two.should_be_equal_to(source.Two.ToString());
        }

        [Test]
        public void source_fields_which_are_strings_can_be_mapped_to_other_types_if_in_correct_format()
        {
            target.Three.ToString().should_be_equal_to(source.Three);
        }

        [Test]
        public void the_source_inheritance_hierarchy_is_flattened()
        {
            target.Four.should_be_equal_to(source.Four);
        }

        [Test]
        public void protected_fields_from_the_source_hierarchy_can_be_mapped()
        {
            target.Five.should_be_equal_to(source.Five);
        }

        [Test]
        public void fields_which_are_not_on_the_source_are_not_changed()
        {
            target.FieldWhichIsNotOnTheSource.should_be_equal_to(10);
        }

        [Test]
        public void fields_are_mapped_to_the_target_hierarchy()
        {
            target.Six.should_be_equal_to(source.Six);
        }
    }

    [TestFixture]
    public class when_the_source_contains_a_field_which_is_not_a_convertible_type_on_the_target : ObjectFieldMapperBaseContext
    {
        protected override void When()
        {
            Subject.Map(new SourceWithFieldOfNonMappableTypeOnTarget(), new TargetWithFieldOfNonMappableTypeFromSource());
        }

        protected override bool RethrowExceptionsThrownDuringWhen
        {
            get
            {
                return false;
            }
        }

        protected override ObjectFieldMapper CreateSubject()
        {
            return new ObjectFieldMapper();
        }

        [Test]
        public void an_exception_should_be_thrown()
        {
            ExceptionThrownDuringWhen.should_be_instance_of_type<InvalidCastException>();
        }
    }

    public class SourceWithFieldOfNonMappableTypeOnTarget
    {
        private int _one = 1;
    }

    public class TargetWithFieldOfNonMappableTypeFromSource
    {
        private TypeFilter _one;
    }

    [TestFixture]
    public class when_a_maping_strategy_is_supplied : ObjectFieldMapperBaseContext
    {
        private IObjectFieldMappingStrategy mapping_strategy;
        private string original_value;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            mapping_strategy = MockRepository.GenerateStub<IObjectFieldMappingStrategy>();
            mapping_strategy.Stub(
               strategy => strategy.ShouldMapField(Arg<FieldInfo>.Matches(field => field.Name == "_one")))
               .Return(false);
            mapping_strategy.Stub(
               strategy => strategy.ShouldMapField(Arg<FieldInfo>.Matches(field => field.Name != "_one")))
               .Return(true)
               .Repeat.Any();
        }

        protected override ObjectFieldMapper CreateSubject()
        {
            return new ObjectFieldMapper(mapping_strategy);
        }

        protected override void When()
        {
            original_value = target.One;
            Subject.Map(source, target);
        }

        [Test]
        public void fields_which_the_strategy_says_should_not_be_mapped_should_not_be_mapped()
        {
            target.One.should_be_equal_to(original_value);
        }
    }
}