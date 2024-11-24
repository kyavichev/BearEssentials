

const fs = require ( 'fs-extra' );


class PackageVersionUpdater
{
    constructor ()
    {
    }


    async updatePackageData()
    {
        const packageJsonPath = './Assets/package.json';

        if ( fs.existsSync( packageJsonPath ) )
        {
            console.log("Found package file here: " + packageJsonPath);

            // Read the package file
            let packageFileString = fs.readFileSync( packageJsonPath, 'utf8' );
            let packageFileJSON = JSON.parse( packageFileString );

            // Version
            let version = packageFileJSON["version"];
            let versionParts = version.split('.');
            let minorVersion = parseInt(versionParts[versionParts.length - 1]);
            minorVersion += 1;
            versionParts[versionParts.length - 1] = "" + minorVersion;
            packageFileJSON["version"] = versionParts.join('.');

            console.log("New version is " + packageFileJSON["version"]);

            // Date
            packageFileJSON["timestamp"] = Date.now();
            packageFileJSON["timestamp_readable"] = new Date(Date.now()).toUTCString();

            // Write lockfile back
            const jsonString = JSON.stringify( packageFileJSON, null, 2 );
            fs.writeFileSync( packageJsonPath, jsonString );

            // Unfortunately, Unity has problems reading the package.json from within packages, so we need to copy this
            const packageCopyPath = './Assets/package_internal.json';
            fs.copyFileSync(packageJsonPath, packageCopyPath);
        }
    }



    async do ()
    {
        console.log("Updating Kons Unity Core package.json");
        await this.updatePackageData();
    }
}


(async () => 
{
    const versionUpdater = new PackageVersionUpdater();
    await versionUpdater.do();
})();



