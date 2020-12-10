namespace AV_Tool
{
    internal class Versioning
    {
        private readonly int _major;
        private readonly int _minor;
        private readonly int _patch;

        public Versioning(string rawVersion)
        {
            var versions = rawVersion.Split('.');

            if (versions.Length != 3)
            {
                return;
            }

            int.TryParse(versions[0], out _major);
            int.TryParse(versions[1], out _minor);
            int.TryParse(versions[2], out _patch);
        }

        public bool IsNewerThan(Versioning version)
        {
            if (_major > version._major)
            {
                return true;
            }

            if (_major != version._major)
            {
                return false;
            }

            if (_minor > version._minor)
            {
                return true;
            }

            return _minor == version._minor && _patch > version._patch;
        }
    }
}