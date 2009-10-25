using GuerillaTactics.Common.Utility;   
using GuerillaTactics.Testing;
using NUnit.Framework;

namespace specs_for_ObjectFieldMapper
{
    [TestFixture]
    public class in_general : Specification<ObjectFieldMaper>
    {
        private Source source;
        private Target target;

        protected override ObjectFieldMaper CreateSubject()
        {
            return new ObjectFieldMaper();
        }

        protected override void When()
        {
            Subject.Map(source, target);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();

            source = new Source {One = "hello", Two = 5, Three = 5.ToString(), Four = 5, Five = 5};
            target = new Target {FieldWhichIsNotOnTheSource = 10};
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
    }

    public abstract class BaseSource
    {
        protected int _five;

        public int Five
        {
            get { return _five; }
            set { _five = value; }
        }

        private int _four;

        public int Four
        {
            get { return _four; }
            set { _four = value; }
        }
    }

    public class Source : BaseSource
    {
        private string _one;
        private int _two;
        private string _three;
        private string _fieldWhichIsNotOnTheTarget;

        public string Three
        {
            get { return _three; }
            set { _three = value; }
        }

        public string One
        {
            get { return _one; }
            set { _one = value; }
        }

        public int Two
        {
            get {
                return _two;
            }
            set {
                _two = value;
            }
        }
    }

    public class Target
    {
        private readonly string _one;
        private string _two;
        private int _three;
        private int _four;
        private int _five;
        private int _fieldWhichIsNotOnTheSource;

        public int FieldWhichIsNotOnTheSource
        {
            get { return _fieldWhichIsNotOnTheSource; }
            set { _fieldWhichIsNotOnTheSource = value; }
        }

        public int Five
        {
            get { return _five; }
        }

        public int Three
        {
            get { return _three; }
        }

        public string One
        {
            get { return _one; }
        }

        public string Two
        {
            get
            {
                return _two;
            }
        }

        public int Four
        {
            get {
                return _four;
            }
        }
    }
}