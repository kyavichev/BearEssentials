
const fs = require ( 'fs-extra' );


class PackageVersionReader
{
	constructor ()
	{
		this.versionNotFound = "0.0.-1";
	}


	async readPackageVersion()
	{
		 const packageJsonPath = './Assets/package.json';

		if ( fs.existsSync( packageJsonPath ) )
		{
			// Read the package file
			let packageFileString = fs.readFileSync( packageJsonPath, 'utf8' );
			let packageFileJSON = JSON.parse( packageFileString );

			// Version
			let version = packageFileJSON["version"];
			return version;
		}

		return this.versionNotFound;
	}



	async do ()
	{
		const version = await this.readPackageVersion();
		return version;
	}
}


(async () => 
{
	const versionReader = new PackageVersionReader();
    const version = await versionReader.do();

    console.log(version);
})();



