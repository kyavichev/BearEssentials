define UpdatePackage
@echo $(shell node ./Scripts/package_version_updater.js)
endef

define GetPackageTag
bear-essentials-$(shell node ./Scripts/package_version_reader.js)
endef


deploy:
	@echo "--------------------------------------------------------------"
	@echo "Creating new Kons Unity Bear Essentials Package"
	$(call UpdatePackage)
	git add Assets/package.json
	git add Assets/package_internal.json
	git commit -m "Updated Kons Unity Bear Essentials package file"
	git push
	git tag $(call GetPackageTag)
	git push origin $(call GetPackageTag)
	