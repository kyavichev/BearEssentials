
const fs = require ( 'fs-extra' );


async function readPackageVersion()
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

	return "0.0.-1"; // Version Not Found;
}


(async () => 
{
    const version = await readPackageVersion();

    console.log(version);
}
)();
